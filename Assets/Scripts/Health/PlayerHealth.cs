using UnityEditor;
using UnityEngine;

public class PlayerHealth : AbstractHealth
{
	bool Invincible = false;
	int InvincibilityTime = 3; //how long player is invincible in seconds

	private void Reset()
	{
		MaxHealth = 3;
	}

	protected override void OnDeath()
	{
		// Placeholder text until formal player death mechanism are discussed.
		Debug.Log("Player died!");
	}

//	void OnCollisionEnter2D(Collision2D collision)
//	{
//		Debug.Log("Collision detected.");
//		if (collision.gameObject.CompareTag("Enemy") && !Invincible)
//		{
//			Debug.Log("Collided w/ Enemy");
//			Damage(collision.gameObject, 1);
//			Invincible = true;
//			Invoke("ResetInvincibility", InvincibilityTime);
//		}
//	}

	void ResetInvincibility()
	{
		Invincible = false;
	}

	public void OnDamage(HealthChangeEvent e)
	{
		if (Invincible)
		{
			e.Cancel();
		}
		else
		{
			Invincible = true;
			Invoke("ResetInvincibility", InvincibilityTime);
		}
	}
}