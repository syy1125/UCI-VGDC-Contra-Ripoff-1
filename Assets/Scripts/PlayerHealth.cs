using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class HealthChangeEvent : UnityEvent<int>
{
}

public class PlayerHealth : MonoBehaviour
{
	// determines the maximum amount of health a player can have.
	public int maxHealth = 3;
	// determines the amount of points the player receives upon touching a health pickup
	public int healthPickUpPoints = 1;
	public bool fullRestoreOnPickUp = false;

	public HealthChangeEvent OnHealthChange;

	private int _health;

	public int Health
	{
		get { return _health; }
		set
		{
			_health = value;
			OnHealthChange.Invoke(_health);
		}
	}

	// Use this for initialization
	void Start()
	{
		Health = maxHealth;
		Health -= 1;  // test line
		Debug.Log(_health);
	}

	private void Update()
	{
		if (_health <= 0)
		{
			Debug.Log("Player is Dead");    // temporary test line until formal death events are discussed
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log(collision.gameObject.tag);
		// if the object it collides with is tagged as an enemy, the player should lose 1 health point.
		if (collision.collider.tag.Equals("Enemy"))
			Health -= 1;

		// players only get health from health pickups if already hurt
		if (/*collision.collider.tag.Equals("HealthPickUp")*/ collision.gameObject.CompareTag("HealthPickUp") && _health < maxHealth)
		{
			// by default, the player receives health points designated by healthPickUpPoints.
			// but, if full restore is turned on (fullRestoreOnPickUp == true), then player is back to full health
			if (!fullRestoreOnPickUp) { Health += healthPickUpPoints; }
			else { Health = maxHealth; }
			Debug.Log(_health);
		}
	}
}