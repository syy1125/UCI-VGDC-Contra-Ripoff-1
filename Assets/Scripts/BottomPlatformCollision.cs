using UnityEngine;

public class BottomPlatformCollision : MonoBehaviour 
{
	private BoxCollider2D parentPlatformCol;

	void Start()
	{
		parentPlatformCol = this.transform.parent.gameObject.GetComponent<BoxCollider2D> ();
	}

	void OnTriggerEnter2D(Collider2D col)//void OnCollisionEnter(Collision col)
	{
		GameObject hit = col.gameObject;
		if (hit != null && hit.CompareTag ("Player"))
			Physics2D.IgnoreCollision (parentPlatformCol, hit.GetComponent<BoxCollider2D> (), true);
	}

	void OnTriggerExit2D(Collider2D col)
	{
		GameObject hit = col.gameObject;
		if (hit != null && hit.CompareTag ("Player")) 
			Physics2D.IgnoreCollision (parentPlatformCol, hit.GetComponent<BoxCollider2D> (), false);
	}
}
