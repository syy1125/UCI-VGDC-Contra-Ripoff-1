using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageProjectile : MonoBehaviour
{
	public bool Hostile;
	public int Damage = 1;
	/// <summary>
	/// Amount of time, in seconds, before the projectile disappears.
	/// Set to negative value to make projectile never expire.
	/// </summary>
	public float Lifespan = 30;
	public float KnockbackBaseStrength = 100;

	private float _startTime;

	void Start()
	{
		_startTime = Time.time;
	}

	void Update()
	{
		//Debug.Log ("updoot");
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
			this.transform.position = new Vector2 (1000000, 10000000);
			//Destroy(gameObject);
		}
	}

	private void Knockback(GameObject target)
	{
		if (target.GetComponent<BoxCollider2D> () == null)
			return;
		//Debug.Log (target.GetComponent<BoxCollider2D> ().bounds.ClosestPoint (this.gameObject.transform.position));
		//Debug.Log (gameObject.GetComponent<CircleCollider2D>().bounds.center);
		Vector2 knockbackDirection = target.GetComponent<BoxCollider2D>().bounds.ClosestPoint(this.gameObject.transform.position) - gameObject.GetComponent<CircleCollider2D>().bounds.center;
		//Debug.Log (knockbackDirection.normalized);
		if (target.CompareTag ("Player")) 
		{
			PlayerMovement p = target.GetComponent<PlayerMovement> ();
			p.stopPlayer ();
			p.removePlayerControl ();
			target.GetComponent<Rigidbody2D> ().AddForce (knockbackDirection.normalized * KnockbackBaseStrength, ForceMode2D.Impulse);
			StartCoroutine (ReturnPlayerControl (p));
		}
		else
			target.GetComponent<Rigidbody2D> ().AddForce (knockbackDirection.normalized * KnockbackBaseStrength, ForceMode2D.Impulse);

		//Vector2 knockbackForce = (velocity + (Vector2) Vector3.Project(velocity, knockbackDirection)) * KnockbackBaseStrength;
		//target.GetComponent<Rigidbody2D>().AddForce(knockbackForce);
	}

	IEnumerator ReturnPlayerControl(PlayerMovement p)
	{
		//yield return new WaitWhile(() =>  p.gameObject.GetComponent<Rigidbody2D>().velocity.x <= float.Epsilon);
		Rigidbody2D r = p.gameObject.GetComponent<Rigidbody2D> ();
		float velX = r.velocity.x;
		while (velX >= PlayerMovement.instance.GetComponent<PlayerMovement>().MovementSpeed) {
			yield return null;
			velX = r.velocity.x;
		}
		p.returnPlayerControl ();
		Destroy (gameObject);
	}
}