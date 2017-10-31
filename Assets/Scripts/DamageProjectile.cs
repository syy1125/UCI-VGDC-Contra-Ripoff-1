using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageProjectile : MonoBehaviour
{
	public bool Hostile;
	public int Damage = 1;

	private void OnTriggerEnter2D(Collider2D other)
	{
		AbstractHealth health = other.gameObject.GetComponent<AbstractHealth>();

		if ((Hostile && health is PlayerHealth) || (!Hostile && health is EnemyHealth))
		{
			DealDamage(health);
		}
	}

	private void DealDamage(AbstractHealth health)
	{
		health.Health -= Damage;
		Destroy(gameObject);
	}
}