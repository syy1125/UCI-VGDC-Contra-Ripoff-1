using UnityEngine;
using UnityEngine.UI;

public class KillCounterHUD : MonoBehaviour
{
	private int _kills;

	/// <summary>
	/// Will automatically be set the the text component's content during initialization.
	/// </summary>
	private string _template;

	private Text _textComponent;

	private void Start()
	{
		_textComponent = gameObject.GetComponent<Text>();
		
		// The text component's current text is to be used as template.
		_template = _textComponent.text;

		SetKills(0);
	}

	private void SetKills(int kills)
	{
		_kills = kills;
		_textComponent.text = string.Format(_template, _kills);
	}

	public void OnKill()
	{
		SetKills(_kills + 1);
	}
}