using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SetHealthEvent : UnityEvent<int>
{
}

public class HealthTemp : MonoBehaviour
{
	public SetHealthEvent OnHealthChange;

	private void Start()
	{
		Invoke("SetHealthLater", 5);
	}

	private void SetHealthLater()
	{
		OnHealthChange.Invoke(3);
	}
}