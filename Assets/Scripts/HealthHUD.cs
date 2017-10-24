using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
	public GameObject HealthCellPrefab;
	public Color HealthyColor = Color.red;
	public Color DepletedColor = Color.gray;

	public int MaxHealth;

	private int _health;

	private void Start()
	{
		_health = MaxHealth;
		UpdateHealthDisplay();
	}

	public void SetMaxHealth(int maxHealth)
	{
		MaxHealth = maxHealth;
		UpdateHealthDisplay();
	}

	public void SetHealth(int health)
	{
		_health = health;
		UpdateHealthDisplay();
	}

	private void UpdateHealthDisplay()
	{
		// Remove all children
		foreach (Transform child in transform)
		{
			Destroy(child);
		}

		int index = 0;
		// Render filled hearts
		for (; index < _health; index++)
		{
			SpawnCell(index, HealthyColor);
		}
		for (; index < MaxHealth; index++)
		{
			SpawnCell(index, DepletedColor);
		}
	}

	private void SpawnCell(int index, Color cellColor)
	{
		GameObject healthCell = Instantiate(HealthCellPrefab);
		healthCell.transform.parent = transform;

		// Position the cell correctly
		Vector3 cellPosition = healthCell.transform.position;
		float cellWidth = healthCell.GetComponent<RectTransform>().rect.width;
		healthCell.transform.position = new Vector3(
			(index + 0.5f) * cellWidth,
			cellPosition.y,
			cellPosition.z
		);

		// Colorize the cell
		healthCell.GetComponent<Image>().color = cellColor;
	}
}