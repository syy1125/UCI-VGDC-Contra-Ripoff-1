using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractHealth : MonoBehaviour
{
	public int MaxHealth;

	private int _health;
	public HealthChangeEventEmitter OnDamage;
	public HealthChangeEventEmitter OnHeal;
	public UnityEvent OnHealthChange;

	public int Health
	{
		get { return _health; }
		set
		{
			if (_health == value) return;

			_health = value;
			OnHealthChange.Invoke();

			if (_health <= 0)
			{
				OnDeath();
			}
		}
	}

	private void Start()
	{
		Health = MaxHealth;
	}

	public bool Damage(GameObject source, int amount)
	{
		HealthChangeEvent healthChangeEvent = new HealthChangeEvent(source, gameObject, -amount);
		OnDamage.Invoke(healthChangeEvent);

		if (!healthChangeEvent.Cancelled)
		{
			Health += healthChangeEvent.HealthDelta;
		}
		else
		{
			Debug.Log("Damage cancelled.");
		}

		return !healthChangeEvent.Cancelled;
	}

	public void Heal(GameObject source, int amount)
	{
		HealthChangeEvent healthChangeEvent = new HealthChangeEvent(source, gameObject, amount);
		OnHeal.Invoke(healthChangeEvent);

		if (!healthChangeEvent.Cancelled)
		{
			Health += healthChangeEvent.HealthDelta;
		}
	}

	protected abstract void OnDeath();
}