using UnityEditor;
using UnityEngine;

public class PlayerMovement : AbstractMovement
{
	[SerializeField] private bool hasDoubleJump;
	private bool _wasGrounded;

	public int jumpForce = 150;

	/// <summary>
	/// The minimum retarding force the player must apply when moving at above standard speeds horizontally.
	/// </summary>
	public float MinResistance = 0.1f;

	private void Start()
	{
		Reset();
	}

	protected override void Reset()
	{
		base.Reset();

		hasDoubleJump = true;
	}

	private void Update()
	{
		float moveHorizontal = Input.GetAxisRaw("Horizontal");

		bool grounded = IsGrounded();

		bool movingReallyFast = false;
		if (Rb.velocity.x < -MovementSpeed)
		{
			moveHorizontal = Mathf.Max(MinResistance, moveHorizontal);
			movingReallyFast = true;
		}
		if (Rb.velocity.x > MovementSpeed)
		{
			moveHorizontal = Mathf.Min(-MinResistance, moveHorizontal);
			movingReallyFast = true;
		}

		if (grounded)
		{
			hasDoubleJump = true;
			if (movingReallyFast)
			{
				Rb.AddForce(new Vector2(moveHorizontal * MovementSpeed, 0));
			}
			else
			{
				Rb.velocity = new Vector2(moveHorizontal * MovementSpeed, Rb.velocity.y);
			}
		}
		else
		{
			Rb.AddForce(new Vector2(moveHorizontal * MovementSpeed, 0));
		}

		if (Input.GetButtonDown("Jump"))
		{
			AttemptJump();
		}
	}

	private void AttemptJump()
	{
		bool grounded = IsGrounded();
		if (!grounded && !hasDoubleJump)
			return;
		if (!grounded)
			hasDoubleJump = false;
		Jump();
	}

	private void Jump()
	{
		Rb.velocity = new Vector2(Rb.velocity.x, 0);
		Rb.AddForce(new Vector2(0, jumpForce));
	}
}