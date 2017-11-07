using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	public GameObject Player;

	public GameObject ProjectilePrefab;

	public float FireInterval = 2;
	private float _fireCooldown;
	public float ProjectileSpeed = 1;

	public int PredictiveAim = 0;
	private Vector2? _playerLastVelocity;
	private Vector2 _playerAcceleration;

	/// <summary>
	/// The enemy will fire at most this degrees above/below the player when trying to lead.
	/// </summary>
	public float FiringArc = 30;

	private void Reset()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Start()
	{
		_fireCooldown = 0;
	}

	private void Update()
	{
		if (_fireCooldown > 0)
		{
			_fireCooldown -= Time.deltaTime;
		}

		if (PredictiveAim == 2)
		{
			// Track player acceleration
			TrackPlayerAccelration();
		}

		if (_fireCooldown <= 0)
		{
			FireAtPlayer();
		}
	}

	private void TrackPlayerAccelration()
	{
		Vector2 playerVelocity = Player.GetComponent<Rigidbody2D>().velocity;
		if (_playerLastVelocity != null)
		{
			_playerAcceleration = (Vector2) ((playerVelocity - _playerLastVelocity) / Time.deltaTime);
		}
		_playerLastVelocity = playerVelocity;
	}

	private void FireAtPlayer()
	{
		GameObject projectile = Instantiate(ProjectilePrefab);

		Vector2 currentPosition = gameObject.transform.position;
		Vector2 target = Player.transform.position;
		float estimatedTimeToImpact = (target - currentPosition).magnitude / ProjectileSpeed;

		// Apply predictive aim
		if (PredictiveAim >= 1)
		{
			target += Player.GetComponent<Rigidbody2D>().velocity * estimatedTimeToImpact;
		}
		if (PredictiveAim >= 2)
		{
			target += _playerAcceleration * 0.5f * estimatedTimeToImpact * estimatedTimeToImpact;
		}

		// Configure projectile
		Vector2 direction = (target - currentPosition);
		// Clamp firing direction
		Vector2 playerOffset = (Vector2) Player.transform.position - currentPosition;
		float angleOffset = Vector2.SignedAngle(playerOffset, direction);
		if (angleOffset < -FiringArc)
		{
			direction = Quaternion.Euler(-FiringArc, 0, 0) * playerOffset;
		}
		if (angleOffset > FiringArc)
		{
			direction = Quaternion.Euler(FiringArc, 0, 0) * playerOffset;
		}

		projectile.GetComponent<DamageProjectile>().Hostile = true;
		projectile.transform.position = currentPosition;
		projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * ProjectileSpeed;

		// Start cooling down
		_fireCooldown = FireInterval;
	}
}