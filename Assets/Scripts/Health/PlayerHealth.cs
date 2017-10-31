using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : AbstractHealth
{
	private void Reset()
	{
		MaxHealth = 3;
	}

	protected override void OnDeath()
	{
		// Placeholder text until formal player death mechanism are discussed.
		Debug.Log("Player died!");
	}
}