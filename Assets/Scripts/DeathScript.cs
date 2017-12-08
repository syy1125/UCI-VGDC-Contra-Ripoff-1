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
		PlayerMovement.instance.GetComponent<PlayerMovement> ().removePlayerControl ();
		//Time.timeScale = 0;
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1;
	}
}