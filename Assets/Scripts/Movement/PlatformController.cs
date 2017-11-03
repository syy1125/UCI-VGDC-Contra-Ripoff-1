using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
	public Collider2D PlayerCollider;
	public Collider2D PlatformCollider;
	public Collider2D PassThroughTrigger;
	private bool _hasPlayer;

	private void Reset()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player != null)
		{
			PlayerCollider = player.GetComponent<Collider2D>();
		}
		PlatformCollider = gameObject.transform.parent.GetComponent<Collider2D>();
		PassThroughTrigger = GetComponent<Collider2D>();
	}

	private void Start()
	{
		StartCoroutine(MainLoop());
	}

	private IEnumerator MainLoop()
	{
		while (true)
		{
			// Wait until player presses down
			yield return new WaitUntil(() => Input.GetAxisRaw("Vertical") < 0);
			// Then disable collision with player
			Physics2D.IgnoreCollision(PlayerCollider, PlatformCollider);
			// Then wait until player releases down
			yield return new WaitWhile(() => Input.GetAxisRaw("Vertical") < 0);
			// Then wait and make sure that player is no longer colliding with the platform
			yield return new WaitWhile(() => PassThroughTrigger.IsTouching(PlayerCollider));
			// Then re-enable collision with player
			Physics2D.IgnoreCollision(PlayerCollider, PlatformCollider, false);
			// Repeat
		}
	}
}