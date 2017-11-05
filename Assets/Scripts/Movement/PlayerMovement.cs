using UnityEngine;

public class PlayerMovement : AbstractMovement
{
	[SerializeField] private bool hasDoubleJump;
	private bool _wasGrounded;
	public LayerMask PlatformMask;

	public int jumpForce = 150;
	public float KnockbackStrengthX = 50;
	public float KnockbackStrengthY = 100;

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

		if (grounded)
		{
			hasDoubleJump = true;
			Rb.velocity = new Vector2(moveHorizontal * MovementSpeed, Rb.velocity.y);
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

	public void Knockback(HealthChangeEvent healthChangeEvent)
	{
		Vector3 sourcePosition = healthChangeEvent.Source.transform.position;
		Vector3 targetPosition = healthChangeEvent.Target.transform.position;

		float xStrength = 0;
		if (sourcePosition.x < targetPosition.x)
		{
			// Attacked from left, knockback to right.
			xStrength = KnockbackStrengthX;
		}
		if (sourcePosition.x > targetPosition.x)
		{
			// Attacked from right, knockback to left.
			xStrength = -KnockbackStrengthX;
		}

		// Apply knockback
		Rb.velocity = new Vector2(Rb.velocity.x, 0);
		Rb.AddForce(new Vector2(xStrength, KnockbackStrengthY));
	}
}