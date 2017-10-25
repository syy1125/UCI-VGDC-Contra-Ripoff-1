using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Events;

public class KillCounterTest : MonoBehaviour
{
	public UnityEvent OnKill;

	private void Start()
	{
		StartCoroutine(KeepAddingKills());
	}

	private IEnumerator KeepAddingKills()
	{
		while (true)
		{
			yield return new WaitForSeconds(2);
			OnKill.Invoke();
		}
	}
}