using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public int baseHealth = 3;   // made this public so it can be modified by the inspector tab.

	private int health;

	// Use this for initialization
	void Start () {
		health = baseHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Returns player's health
	public int GetHealth()
	{
		return health;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// if the object it collides with is tagged as an enemy, the player should losw 1 health point.
		if (collision.collider.tag.Equals("Enemy"))
			health--;

		if (collision.collider.tag.Equals("HealthPickUp"))
			health++;
	}
}
