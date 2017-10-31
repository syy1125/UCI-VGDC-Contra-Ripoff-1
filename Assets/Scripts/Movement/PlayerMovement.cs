using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : AbstractMovement
{
	[SerializeField] private bool hasDoubleJump;

	public int jumpForce = 150;
	
	/*[HideInInspector]*/
	public TopPlatformCollision currentPlatform;

	private void Start()
	{
		hasDoubleJump = true;
		currentPlatform = null;
	}

	private void Update()
	{
		float moveHorizontal = Input.GetAxisRaw("Horizontal");
		float moveVertical = Input.GetAxisRaw("Vertical");

		Rb.velocity = new Vector2(moveHorizontal * MovementSpeed, Rb.velocity.y);

		if (Input.GetButtonDown("Jump"))
			AttemptJump();

		if (IsGrounded())
			hasDoubleJump = true;

		if (moveVertical < 0 && currentPlatform != null)
			currentPlatform.DropThrough(Collider);
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