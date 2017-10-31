using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageProjectile : MonoBehaviour
{
	public bool Hostile;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (Hostile && other.gameObject.CompareTag("Player"))
		{
			PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

			if (playerHealth != null)
			{
				playerHealth.Health--;
				Destroy(gameObject);
			}
		}
		else if (!Hostile && other.gameObject.CompareTag("Enemy"))
		{
			Enemy enemy = other.gameObject.GetComponent<Enemy>();

			if (enemy != null)
			{
				enemy.Health--;
				Destroy(gameObject);
			}
		}
	}
}