using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpingEnemy : EnemyMovement
{
	public int jumpForce;
	public float jumpingTolerance; // represents how close the player is before it tries to jump
	public int timeBetweenJumps; //how long in between jumps

	[SerializeField]private float jumpCooldown;

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();

		if (ShouldJump() && jumpCooldown <= 0)
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

	bool ShouldJump()
	{
		float playerPositionY = Player.transform.position.y;
		float enemyPositionY = this.transform.position.y;
		float playerPositionX = Player.transform.position.x;
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
		if (!IsGrounded())
			return;
		Jump ();
	}

	void Jump()
	{
		Rb.velocity = new Vector2(Rb.velocity.x, 0);
		Rb.AddForce (new Vector2(0, jumpForce));
	}
}
