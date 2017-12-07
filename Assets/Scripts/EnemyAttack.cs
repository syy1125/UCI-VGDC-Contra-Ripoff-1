using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	public GameObject Player;

	public GameObject ProjectilePrefab;

	public float FireInterval = 2;
	private float _fireCooldown;
	public float ProjectileSpeed = 1;

	public int PredictiveAim = 0;
	private Vector2[] _playerTransformDerivatives;

	/// <summary>
	/// The enemy will fire at most this degrees above/below the player when trying to lead.
	/// </summary>
	public float FiringArc = 30;

	void Reset()
	{
		Player = PlayerMovement.instance;
	}

	void Start()
	{
		Reset ();
		_fireCooldown = 0;
		_playerTransformDerivatives = new Vector2[PredictiveAim + 1];
	}

	private void Update()
	{
		TrackPlayerMotionDerivatives();

		if (_fireCooldown > 0)
		{
			_fireCooldown -= Time.deltaTime;
		}

		if (_fireCooldown <= 0)
		{
			FireAtPlayer();
		}
	}

	private void TrackPlayerMotionDerivatives()
	{
		Vector2[] newDerivatives = new Vector2[PredictiveAim + 1];

		newDerivatives[0] = Player.transform.position;
		for (int order = 1; order <= PredictiveAim; order++)
		{
			newDerivatives[order] = (newDerivatives[order - 1] - _playerTransformDerivatives[order - 1]) / Time.deltaTime;
		}

//		Debug.Log(String.Join(",", newDerivatives.Select(vector => vector.ToString()).ToArray()));
		_playerTransformDerivatives = newDerivatives;
	}

	private Vector2 GetAimLocation()
	{
		int taylorDenominator = 1;
		Vector2 position = _playerTransformDerivatives[0];
		float estimatedTravelTime = (position - (Vector2) transform.position).magnitude / ProjectileSpeed;
		float timeMultiplier = 1;

		for (int order = 1; order <= PredictiveAim; order++)
		{
			taylorDenominator *= order;
			timeMultiplier *= estimatedTravelTime;
			position += _playerTransformDerivatives[order] * timeMultiplier / taylorDenominator;
		}

		return position;
	}

	private void FireAtPlayer()
	{
		GameObject projectile = Instantiate(ProjectilePrefab);
		Vector2 currentPosition = transform.position;
		Vector2 target = GetAimLocation();

		Vector2 direction = target - currentPosition;

		// Clamp firing direction
		Vector2 playerOffset = (Vector2) Player.transform.position - currentPosition;
		float angleOffset = Vector2.SignedAngle(playerOffset, direction);
		if (angleOffset < -FiringArc) direction = Quaternion.Euler(-FiringArc, 0, 0) * playerOffset;
		if (angleOffset > FiringArc) direction = Quaternion.Euler(FiringArc, 0, 0) * playerOffset;

		projectile.GetComponent<DamageProjectile>().Hostile = true;
		projectile.transform.position = currentPosition;
		projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * ProjectileSpeed;

		// Start cooling down
		_fireCooldown = FireInterval;
	}
}