using UnityEngine;
using UnityEngine.UI;

public class HealthHud : Singleton<HealthHud>
{
	public GameObject HealthCellPrefab;
	public Color HealthyColor = Color.red;
	public Color DepletedColor = Color.gray;

	/// <summary>
	/// Where the health cells start. The components are a multiplier for the cell's width and height.
	/// </summary>
	public Vector2 CellStart = new Vector2(0.5f, 0.5f);

	/// <summary>
	/// What direction the health cells will stretch out.The components are a multiplier for the cell's width and height.
	/// </summary>
	public Vector2 CellDirection = Vector2.right;

	public PlayerHealth Health;

	private void Start()
	{
		UpdateHealthDisplay();
	}

	public void UpdateHealthDisplay()
	{
		// Remove all children
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}

		int index = 0;
		// Render filled hearts
		for (; index < Health.Health; index++)
		{
			SpawnCell(index, HealthyColor);
		}
		// Render depleted hearts
		for (; index < Health.MaxHealth; index++)
		{
			SpawnCell(index, DepletedColor);
		}
	}

	private void SpawnCell(int index, Color cellColor)
	{
		GameObject healthCell = Instantiate(HealthCellPrefab);
		healthCell.transform.SetParent(transform);

		// Position the cell correctly
		Vector3 cellPosition = healthCell.transform.position;
		float cellWidth = healthCell.GetComponent<RectTransform>().rect.width;
		float cellHeight = healthCell.GetComponent<RectTransform>().rect.width;

		healthCell.transform.position = new Vector3(
			(index * CellDirection.x + CellStart.x) * cellWidth,
			(index * CellDirection.y + CellStart.y) * cellHeight,
			cellPosition.z
		);

		// Colorize the cell
		healthCell.GetComponent<Image>().color = cellColor;
	}
}