using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour {

	public GameObject proj;

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
			p.GetComponent<Rigidbody2D> ().velocity = new Vector2(0.2f,0f);
			yield return new WaitForSeconds (1f);
		}
	}
}
