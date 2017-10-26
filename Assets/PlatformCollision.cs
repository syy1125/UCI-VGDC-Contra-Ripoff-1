using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollision : MonoBehaviour 
{
	private BoxCollider2D parentPlatformCol;

	void Start()
	{
		parentPlatformCol = this.transform.parent.gameObject.GetComponent<BoxCollider2D> ();
		Debug.Log (parentPlatformCol);
	}

	void OnTriggerEnter2D(Collider2D col)//void OnCollisionEnter(Collision col)
	{
		Debug.Log ("ping");
		GameObject hit = col.gameObject;
		if (hit != null && hit.CompareTag ("Player"))
			Physics2D.IgnoreCollision (parentPlatformCol, hit.GetComponent<BoxCollider2D> (), true);
	}

	void OnTriggerExit2D(Collider2D col)
	{
		Debug.Log ("pong");
		GameObject hit = col.gameObject;
		if (hit != null && hit.CompareTag ("Player")) 
			Physics2D.IgnoreCollision (parentPlatformCol, hit.GetComponent<BoxCollider2D> (), false);
	}
}
