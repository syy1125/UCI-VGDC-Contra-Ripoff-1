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
	
	// determines how far an enemy can detect an object above it (this is used as the distance parameter when using Raycast in the ShouldJump() method)
	public float SafeToJumpDetectionDistance = 1.6f;
	// determines how far an enemy can detect an object below it (this is used as the distance parameter when using Raycast in the ShouldFall() method)
	public float FallDetectionDistance = 10f;

	protected ContactFilter2D filter;
	protected bool isFalling;
	protected float currentGroundYScale;

	protected float prevXvelocity;
	protected float prevYvelocity;
	protected bool haltVelocity;

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
		currentGroundYScale = 0.2f;
		haltVelocity = false;
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
		if (!haltVelocity)
		{
			prevXvelocity = Rb.velocity.x;
			prevYvelocity = Rb.velocity.y;
			Rb.velocity = new Vector2(prevXvelocity, prevYvelocity);
		}

		if (!IsGrounded())
			isFalling = true;
		else
		{
			isFalling = false;
			if (!Collider.IsTouching(Floor))
				currentGroundYScale = 1.0f;
			else
				currentGroundYScale = 0.2f;
		}

		Rb.velocity = new Vector2(CalcXVelocity(), Rb.velocity.y);
		Debug.Log(gameObject.name + ": velocity = " + Rb.velocity);

		if (ShouldJump() && jumpCooldown <= 0)
		{
			AttemptJump();
			jumpCooldown = timeBetweenJumps;
			StartCoroutine("JumpCD");
		}

//		if (ShouldFall())
//		{
//			isFalling = true;
//			if (FloorLayerUsed)
//			{
//				StartCoroutine(FallThroughPlatform());
//			}
//			else
//			{
//				Collider2D[] contacts = new Collider2D[1];
//				Physics2D.GetContacts(Collider, filter, contacts);
//				//Debug.Log(contacts[0].name);
//				if (!(contacts.Length == 0) /*&& contacts[0].gameObject.tag.Equals("Ground")*/)
//				{
//					StartCoroutine(FallThroughPlatform(contacts[0]));
//				}
//				Debug.Log("number of enemy contacts: " + contacts.Length);
//			}
//		}
	}

	protected virtual void CalculateTotalVelocity()
	{
		Vector2 newVelocity;
		RaycastHit2D hitEnemy;

		if (IsGrounded())
		{
			hitEnemy = Physics2D.CircleCast(transform.position + transform.up.normalized * 0.1f, 0.4f, transform.up.normalized, 1.0f, gameObject.layer);
			if (hitEnemy.collider == null)
			{
				newVelocity = new Vector2(CalcXVelocity(), Rb.velocity.y);
			}
			else
			{
				float otherEnemyPosX = hitEnemy.collider.gameObject.transform.position.x;
				if (transform.position.x < otherEnemyPosX)
					newVelocity = new Vector2(-MovementSpeed, Rb.velocity.y);
				else
					newVelocity = new Vector2(-MovementSpeed, Rb.velocity.y);
			}
		}

		else
		{
			hitEnemy = Physics2D.CircleCast(transform.position - transform.up.normalized * 0.1f, 0.4f, -transform.up.normalized, 1.0f, gameObject.layer);
			if (hitEnemy.collider == null)
			{
				haltVelocity = false;
				newVelocity = new Vector2(prevXvelocity, prevYvelocity);
				newVelocity = Rb.velocity;
			}
			else
			{
				if (!haltVelocity)
				{
					prevXvelocity = Rb.velocity.x;
					prevYvelocity = Rb.velocity.y;
				}

				haltVelocity = true;
				newVelocity = new Vector2(0.0f, 0.0f);
			}
		}

		Rb.velocity = newVelocity;
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
		float collisionX = collision.transform.position.x;
		float collisionY = collision.transform.position.y;
		float posX = this.transform.position.x;
		float posY = this.transform.position.y;
		// if object lands on top of another enemy, then the object should move itself to be back on ground level
		if (collision.collider.tag.Equals("Enemy") && collisionY < posY)
		{
			transform.Translate(new Vector3(transform.position.x - collisionX, transform.position.y - collisionY /*collisionY + 0.01f*/));
			if (!IsGrounded())
				transform.Translate(new Vector3(collisionX - transform.position.x, collisionY - transform.position.y));
			//transform.Translate(new Vector3(collisionX + 0.1f, transform.position.y - collisionY /*+ 0.5f*/));
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
		if (!IsSafeToChangePlatforms(transform.position + transform.up.normalized * 0.18f, transform.up.normalized, SafeToJumpDetectionDistance))
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

	protected override bool ShouldPassThroughPlatform()
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
			&& IsGrounded()
			&& IsSafeToChangePlatforms(transform.position - transform.up.normalized * (0.18f + currentGroundYScale),
				transform.up.normalized * -1.0f, SafeToJumpDetectionDistance))
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

		Debug.Log(name + ": will fall? = " + fall);
		return fall;
	}

	bool IsSafeToChangePlatforms(Vector2 rayOrigin, Vector2 rayDir, float rayDistance)
	{
		bool safe = true;
		// casts a ray to get info on the first platform the ray hits within rayDistance
		//RaycastHit2D hitPlatform = Physics2D.Raycast(rayOrigin, rayDir, rayDistance, GroundMask.value);
		RaycastHit2D hitPlatform = Physics2D.Raycast(rayOrigin, rayDir);

		// should not jump if a platform is not in range
		if (hitPlatform.collider == null) safe = false;
		//else if (hitPlatform.collider.name.Equals("Floor"))
		//	return false;

		// casts a ray to find an enemy, if hitEnemy contains info on an enemy, then it delays moving to hopefully not collide an another enemy.
		RaycastHit2D hitEnemy = Physics2D.Raycast(rayOrigin, rayDir, rayDistance, 1 << gameObject.layer);
		if (hitEnemy.collider != null)
		{
			if (IsGrounded())
				Rb.velocity = new Vector2(0.0f, Rb.velocity.y);
			safe = false;
		}

		// there is a platform to land on, and no enemies are in the way, so it is safe to move to another platform
		Debug.Log(name + ": safe to change platforms? = " + safe);
		return safe;
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