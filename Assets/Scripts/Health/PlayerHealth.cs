using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		// Relaod scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

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