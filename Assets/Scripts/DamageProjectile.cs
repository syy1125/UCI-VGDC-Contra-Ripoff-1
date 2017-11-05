using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;

public class DamageProjectile : MonoBehaviour
{
	public bool Hostile;
	public int Damage = 1;

	/// <summary>
	/// Amount of time, in seconds, before the projectile disappears.
	/// Set to negative value to make projectile never expire.
	/// </summary>
	public float Lifespan = 30;

	private float _startTime;

	private void Start()
	{
		_startTime = Time.time;
	}

	private void Update()
	{
		if (Lifespan > 0 && Time.time > _startTime + Lifespan)
		{
			Destroy(gameObject);
		}
	}

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
		if (health.Damage(gameObject, Damage))
		{
			// Remove the projectile if damade dealt successfully.
			Destroy(gameObject);
		}
	}
}