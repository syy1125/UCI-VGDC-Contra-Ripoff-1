using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScript : Singleton<DeathScript>
{
	public void Show()
	{
		gameObject.GetComponent<Image>().enabled = true;
		foreach (Transform t in transform)
		{
			t.gameObject.SetActive(true);
		}
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}