using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour {

	public GameObject proj;
	public float CD;
	public float BulletSpeed;
	public Vector2 BulletDirection;

	void Start () 
	{
		StartCoroutine (Shoot ());
	}
	
	IEnumerator Shoot()
	{
		while (true) 
		{
			GameObject p = Instantiate (proj, transform.position, Quaternion.identity, transform);
			BulletScript b = p.GetComponent<BulletScript> ();
			b.direction = BulletDirection;
			b.Speed = BulletSpeed;
			//p.GetComponent<Rigidbody2D>().velocity = BulletDirection.normalized * BulletSpeed;
			yield return new WaitForSeconds (CD);
		}
	}
}
