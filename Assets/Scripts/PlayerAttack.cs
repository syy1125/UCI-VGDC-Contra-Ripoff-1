using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public GameObject ProjectilePrefab;

	/// <summary>
	/// Amount of time, in seconds, between the layer being able to shoot.
	/// </summary>
	public float FireInterval = 1;

	private float _fireColdown = 0;

	public float ProjectileSpeed = 1;

	private void Update()
	{
		_fireColdown -= Time.deltaTime;

		if (Input.GetMouseButton(0) && _fireColdown <= 0)
		{
			GameObject projectile = Instantiate(ProjectilePrefab);

			Vector2 aimPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 projectileDirection = (aimPosition - (Vector2) transform.position).normalized;

			// Projectile configuration
			projectile.GetComponent<DamageProjectile>().Hostile = false;
			projectile.transform.position = transform.position;
			projectile.GetComponent<Rigidbody2D>().velocity = projectileDirection * ProjectileSpeed;

			// Set cooldown
			_fireColdown = FireInterval;
		}
	}
}