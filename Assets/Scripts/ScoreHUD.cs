using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHUD : MonoBehaviour
{
	/// <summary>
	/// A template string to be formatted with the score.
	/// </summary>
	private string _template;

	private Text _textComponent;

	private void Start()
	{
		_textComponent = gameObject.GetComponent<Text>();
		
		// The text component's current text is to be used as template.
		_template = _textComponent.text;

		SetScore(0);
	}

	public void SetScore(int score)
	{
		_textComponent.text = string.Format(_template, score);
	}
}