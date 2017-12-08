using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneTrigger : MonoBehaviour {

	public Vector3 TeleportLocation;
	public int Damage = 1;

	void Start () 
	{
		
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerHealth> ().Damage (this.gameObject, Damage);
			other.gameObject.transform.position = TeleportLocation;
		} else if (other.gameObject.tag == "Enemy") {
			Destroy (other.gameObject);
		}
	}
}
