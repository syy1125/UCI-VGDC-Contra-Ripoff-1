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
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	IEnumerator Shoot()
	{
		while (true) 
		{
			GameObject p = Instantiate (proj, this.transform.position, Quaternion.identity, this.transform);
			BulletScript temp = p.GetComponent<BulletScript> ();
			p.GetComponent<Rigidbody2D>().velocity = BulletDirection.normalized * BulletSpeed;
//			temp.Speed = BulletSpeed;
//			temp.direction = BulletDirection;
			yield return new WaitForSeconds (CD);
		}
	}
}
