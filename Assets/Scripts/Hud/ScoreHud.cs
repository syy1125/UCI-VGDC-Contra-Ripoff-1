﻿using UnityEngine.UI;

public class ScoreHud : Singleton<ScoreHud>
{
	/// <summary>
	/// Will automatically be set the the text component's content during initialization.
	/// </summary>
	private string _template;

	private Text _textComponent;
    public int ScoreCount = 0;

	private void Start()
	{
		_textComponent = gameObject.GetComponent<Text>();

		// The text component's current text is to be used as template.
		_template = _textComponent.text;

		SetScore(ScoreCount);
	}

	public void SetScore(int score)
	{
        ScoreCount += score;
		_textComponent.text = string.Format(_template, ScoreCount);
	}
}