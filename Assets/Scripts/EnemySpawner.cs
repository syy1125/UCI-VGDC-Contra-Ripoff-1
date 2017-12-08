using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{

	public GameObject EnemyToCreate;

	private float CreationRadius;
	private Camera main;
	private CircleCollider2D col;
	private bool spawned;

	void Start () 
	{
		col = GetComponent<CircleCollider2D>();
		/*main = Camera.main;
		Vector3 edgeOfScreen = main.ScreenToWorldPoint(new Vector2(main.pixelWidth/2,0));
		//CreationRadius = main.ScreenToWorldPoint();
		Debug.Log(edgeOfScreen);
		//col.radius = CreationRadius;
		*/
		spawned = false;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(!spawned && other.gameObject.tag == "Player")
		{
			spawned = true;
			Instantiate(EnemyToCreate, this.transform.position, Quaternion.identity);
		}
	}
}
