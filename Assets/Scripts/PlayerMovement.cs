using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	private Rigidbody2D rb;

	public int jumpForce = 150;
	public int movementSpeed = 5;

	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");

		rb.velocity = new Vector2(moveHorizontal * movementSpeed, rb.velocity.y);

		if (Input.GetButtonDown ("Jump"))
			this.rb.AddForce ( new Vector2(0,jumpForce));

	}
}
