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

	public float KnockbackBaseStrength = 100;

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
			Knockback(health.gameObject);
			Destroy(gameObject);
		}
	}

	private void Knockback(GameObject target)
	{
		//Vector2 knockbackDirection = this.GetComponent<BulletScript> ().direction;
		if (target.GetComponent<BoxCollider2D> () == null)
			return;
		Vector2 knockbackDirection = target.GetComponent<BoxCollider2D>().bounds.ClosestPoint(this.gameObject.transform.position) - gameObject.transform.position;
		Debug.Log (knockbackDirection.normalized);
		//Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
		target.GetComponent<Rigidbody2D> ().AddForce (knockbackDirection.normalized * KnockbackBaseStrength, ForceMode2D.Impulse);
		//Vector2 knockbackForce = (velocity + (Vector2) Vector3.Project(velocity, knockbackDirection)) * KnockbackBaseStrength;
		//target.GetComponent<Rigidbody2D>().AddForce(knockbackForce);
	}
}