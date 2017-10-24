using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ScoreChangeEvent : UnityEvent<int>
{
}

public class ScoreTest : MonoBehaviour
{
	public ScoreChangeEvent OnScoreChange;
	
	// Use this for initialization
	void Start()
	{
		StartCoroutine(KeepAddingScore());
	}

	private IEnumerator KeepAddingScore()
	{
		for (int i = 0;; i++)
		{
			yield return new WaitForSeconds(0.1f);
			OnScoreChange.Invoke(i);
		}
	}
}