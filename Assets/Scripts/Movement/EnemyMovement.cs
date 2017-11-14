using System.Collections;
using UnityEngine;

//This script controls makes an enemy that only moves towards a player.
// EDIT: script now makes an enemy move and/or jump towards a player.

public class EnemyMovement : AbstractMovement
{
	public float MoveThreshold;
	public GameObject Player;
	public Collider2D Floor;
	public LayerMask FloorMask;
	public bool FloorLayerUsed = false;   // this variable determines whether we use a separate layer(true) or store the floor object (false) to stop enemy from falling through the floor

	public int jumpForce = 300;
	public float jumpingTolerance = 1; // represents how close the player is before it tries to jump
	public int timeBetweenJumps = 3; //how long in between jumps
	public float fallTolerance = 2; // represents how close the player is before it tries to fall

	protected ContactFilter2D filter;
	protected bool isFalling;

	[SerializeField] private float jumpCooldown;
	//private Collider2D platform;

	/*protected override void Start()
	{
		base.Start();

		if (Player == null)
			Player = GameObject.FindGameObjectWithTag("Player");
	}*/

	protected override void Start()
	{
		base.Reset();
		base.Start();

		if (Player == null)
			Player = GameObject.FindGameObjectWithTag("Player");

		// temporary until floor uses a different layer!!!!
		if (Floor == null)
		{
			Floor = GameObject.Find("Floor").GetComponent<Collider2D>();
		}

		ContactFilter2D filter = new ContactFilter2D();
		filter.useLayerMask = true;
		filter.layerMask = GroundMask;
		isFalling = false;
	}

	protected override void Reset()
	{
		base.Reset();

		MoveThreshold = MovementSpeed;
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	private float CalcXVelocity()
	{
		float xDiff = Player.transform.position.x - transform.position.x;

		if (xDiff > MoveThreshold)
		{
			return MovementSpeed;
		}
		else if (xDiff < -MoveThreshold)
		{
			return -MovementSpeed;
		}
		else
		{
			return 0;
		}
	}

	// Update is called once per frame
	protected virtual void Update()
	{
		if (!IsGrounded())
			isFalling = true;
		else
			isFalling = false;

		Rb.velocity = new Vector2(CalcXVelocity(), Rb.velocity.y);

		if (ShouldJump() && jumpCooldown <= 0)
		{
			AttemptJump();
			jumpCooldown = timeBetweenJumps;
			StartCoroutine("JumpCD");
		}

		if (ShouldFall())
		{
			isFalling = true;
			if (FloorLayerUsed)
			{
				StartCoroutine(FallThroughPlatform());
			}
			else
			{
				Collider2D[] contacts = new Collider2D[1];
				Physics2D.GetContacts(Collider, filter, contacts);
				//Debug.Log(contacts[0].name);
				if (!(contacts.Length == 0) /*&& contacts[0].gameObject.tag.Equals("Ground")*/)
				{
					StartCoroutine(FallThroughPlatform(contacts[0]));
				}
				Debug.Log("number of enemy contacts: " + contacts.Length);
			}
		}
	}

	IEnumerator JumpCD()
	{
		while (jumpCooldown > 0)
		{
			jumpCooldown -= Time.deltaTime;
			yield return null;
		}
		jumpCooldown = 0;
	}

	bool ShouldJump()
	{
		float playerPositionY = Player.transform.position.y;
		float enemyPositionY = this.transform.position.y;
		float playerPositionX = Player.transform.position.x;
		float enemyPositionX = this.transform.position.x;

		bool jump = false;
		if (playerPositionY > enemyPositionY)
			jump = true;
		if (Mathf.Abs(playerPositionY - enemyPositionY) <= 0.1)
			jump = false;
		if (Mathf.Abs(playerPositionX - enemyPositionX) >= jumpingTolerance)
			jump = false;
		Debug.Log(jump);
		return jump;
	}

	void AttemptJump()
	{
		if (!IsGrounded())
			return;
		Jump();
	}

	void Jump()
	{
		Rb.velocity = new Vector2(Rb.velocity.x, 0);
		Rb.AddForce(new Vector2(0, jumpForce));
	}

	bool ShouldFall()
	{
		float playerPositionY = Player.transform.position.y;
		float enemyPositionY = this.transform.position.y;
		float playerPositionX = Player.transform.position.x;
		float enemyPositionX = this.transform.position.x;

		bool fall = false;
		if (/*!isFalling
			&&*/ (playerPositionY < enemyPositionY) 
			&& (Mathf.Abs(playerPositionY - enemyPositionY) > 0.1)
			&& !(Mathf.Abs(playerPositionX - enemyPositionX) >= fallTolerance) 
			&& IsGrounded())
		{
			if (FloorLayerUsed)
			{
				if (!Collider.IsTouchingLayers(FloorMask.value))
					fall = true;
			}

			else
			{
				if (!Collider.IsTouching(Floor))
					fall = true;
			}
		}

		return fall;
	}

	IEnumerator FallThroughPlatform()
	{
		//Physics2D.IgnoreCollision(Collider, platform);
		Collider.isTrigger = true;
		yield return new WaitWhile(() => IsGrounded());
		//Physics2D.IgnoreCollision(Collider, platform, false);
		Collider.isTrigger = false;
	}

	IEnumerator FallThroughPlatform(Collider2D platform)
	{
		Physics2D.IgnoreCollision(Collider, platform);
		//Collider.isTrigger = true;
		yield return new WaitWhile(() => Collider.IsTouching(platform));
		Physics2D.IgnoreCollision(Collider, platform, false);
		//Collider.isTrigger = false;
	}
}