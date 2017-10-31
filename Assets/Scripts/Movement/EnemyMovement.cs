using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls makes an enemy that only moves towards a player.

public class EnemyMovement : AbstractMovement
{
	public float MoveThreshold;
	public GameObject Player;

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
		Rb.velocity = new Vector2(CalcXVelocity(), Rb.velocity.y);
	}
}