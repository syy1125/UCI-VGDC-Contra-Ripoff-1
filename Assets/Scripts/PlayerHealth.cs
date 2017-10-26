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
	public int baseHealth = 3; // made this public so it can be modified by the inspector tab.

	public HealthChangeEvent OnHealthChange;

	private int _health;

	public int Health
	{
		get { return _health; }
		private set
		{
			_health = value;
			OnHealthChange.Invoke(_health);
		}
	}

	// Use this for initialization
	void Start()
	{
		Health = baseHealth;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// if the object it collides with is tagged as an enemy, the player should lose 1 health point.
		if (collision.collider.tag.Equals("Enemy"))
			Health--;

		if (collision.collider.tag.Equals("HealthPickUp"))
			Health++;
	}
}