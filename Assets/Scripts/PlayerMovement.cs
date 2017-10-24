using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	private Rigidbody2D rb;
	private BoxCollider2D col;

	[SerializeField]private bool hasDoubleJump;

	public int jumpForce = 150;
	public int movementSpeed = 5;

	void Start () 
	{
		hasDoubleJump = true;
		rb = GetComponent<Rigidbody2D> ();
		col = GetComponent<BoxCollider2D> ();
	}

	void Update ()
	{
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		rb.velocity = new Vector2(moveHorizontal * movementSpeed, rb.velocity.y);

		if (Input.GetButtonDown ("Jump"))
			AttemptJump ();
	}

	void AttemptJump()
	{
		if (isGrounded ())
			hasDoubleJump = true;
		else if (hasDoubleJump)
			hasDoubleJump = false;
		else
			return;
		Jump ();

		
	}

	void Jump()
	{
		rb.velocity = new Vector2(rb.velocity.x, 0);
		rb.AddForce ( new Vector2(0,jumpForce));
	}

	bool isGrounded()
	{
		Vector2 bottom = (Vector2)col.bounds.ClosestPoint (transform.position + Vector3.down * 3);
		RaycastHit2D temp = Physics2D.Raycast(bottom, Vector2.down, 0.1f);
		if(temp.transform != null && temp.transform.gameObject.CompareTag("Ground"))
			return true;
		return false;
	}
}
