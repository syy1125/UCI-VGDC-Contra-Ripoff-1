using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpingEnemy : Enemy 
{

	public float speed = 0.01f;
	public int jumpForce;
	public LayerMask groundCollisions;
	public float jumpingTolerance; // represents how close the player is before it tries to jump
	public int timeBetweenJumps; //how long in between jumps
	private GameObject player;
	private Rigidbody2D rb;
	private BoxCollider2D col;

	[SerializeField]private float jumpCooldown;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");
		col = GetComponent<BoxCollider2D>();
	}

	// Update is called once per frame
	void Update ()
	{
		rb.velocity = new Vector2(DetermineXDirection() * speed, rb.velocity.y);

		if(ShouldJump() && jumpCooldown <= 0)
		{
			AttemptJump();
			jumpCooldown = timeBetweenJumps;
			StartCoroutine("JumpCD");
		}
	}

	IEnumerator JumpCD()
	{
		while(jumpCooldown > 0)
		{
			jumpCooldown -= Time.deltaTime;
			yield return null;
		}
		jumpCooldown = 0;
	}

	float DetermineXDirection()
	{

		float playerPositionX = player.transform.position.x;
		float enemyPositionX = this.transform.position.x;
		float xDirection;
		if(Mathf.Abs(playerPositionX  - enemyPositionX) <= 0.01)
			xDirection = 0;
		else if(playerPositionX  < enemyPositionX)
			xDirection = -1.0f;
		else
			xDirection = 1.0f;
		return xDirection;
	}

	bool ShouldJump()
	{
		float playerPositionY = player.transform.position.y;
		float enemyPositionY = this.transform.position.y;
		float playerPositionX = player.transform.position.x;
		float enemyPositionX = this.transform.position.x;
		bool jump = false;
		if(playerPositionY > enemyPositionY)
			jump = true;
		if(Mathf.Abs(playerPositionY - enemyPositionY) <= 0.1)
			jump = false;
		if(Mathf.Abs(playerPositionX - enemyPositionX) >= jumpingTolerance)
			jump = false;
		Debug.Log(jump);
		return jump;
	}

	void AttemptJump()
	{
		if (!isGrounded())
			return;
		Jump ();
	}

	void Jump()
	{
		rb.velocity = new Vector2(rb.velocity.x, 0);
		rb.AddForce (new Vector2(0, jumpForce));
	}

	bool isGrounded()
	{
		Vector2 bottom = (Vector2)col.bounds.ClosestPoint (transform.position + (Vector3.down * 5));
		int layerMask = groundCollisions.value;
		RaycastHit2D temp = Physics2D.Raycast(bottom, Vector2.down, 0.05f, layerMask);
		return temp.transform != null && temp.transform.gameObject.CompareTag ("Ground");
	}


}
