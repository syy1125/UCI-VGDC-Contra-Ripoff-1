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
	public LayerMask groundCollisions;

	/*[HideInInspector]*/public TopPlatformCollision currentPlatform;

	void Start () 
	{
		hasDoubleJump = true;
		rb = GetComponent<Rigidbody2D> ();
		col = GetComponent<BoxCollider2D> ();
		currentPlatform = null;
	}

	void Update ()
	{
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		rb.velocity = new Vector2(moveHorizontal * movementSpeed, rb.velocity.y);

		if (Input.GetButtonDown ("Jump"))
			AttemptJump ();
		if(isGrounded())
			hasDoubleJump = true;
		if(moveVertical < 0 && currentPlatform != null)
			currentPlatform.DropThrough(col);
			
	}

	void AttemptJump()
	{
		if (!isGrounded () && !hasDoubleJump)
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
		RaycastHit2D temp = Physics2D.Raycast(bottom, Vector2.down, 0.1f, layerMask);
			return temp.transform != null && temp.transform.gameObject.CompareTag ("Ground");
	}
}
