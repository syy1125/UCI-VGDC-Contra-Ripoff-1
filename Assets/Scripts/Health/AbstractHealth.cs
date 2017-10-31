using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractHealth : MonoBehaviour
{
	public int MaxHealth;

	private int _health;
	public UnityEvent OnHealthChange;

	public int Health
	{
		get { return _health; }
		set
		{
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

	protected abstract void OnDeath();
}