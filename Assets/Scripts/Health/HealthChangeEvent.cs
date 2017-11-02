using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class HealthChangeEventEmitter : UnityEvent<HealthChangeEvent>
{
}

/// <summary>
/// An event that's emitted when the player's health is about to change.
/// This event is mutable and cancellable, for buffs and debuffs to potentiaally cancel or alter a change to player's health.
/// </summary>
public class HealthChangeEvent
{
	public bool Cancelled { get; private set; }
	public readonly GameObject Source;
	public readonly GameObject Target;
	public int HealthDelta;

	public HealthChangeEvent(GameObject source, GameObject target, int healthDelta)
	{
		Source = source;
		Target = target;
		HealthDelta = healthDelta;
	}

	public void Cancel()
	{
		Cancelled = true;
	}
}