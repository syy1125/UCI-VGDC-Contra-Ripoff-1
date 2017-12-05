using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoHud : Singleton<AmmoHud>
{
	public GameObject AmmoPrefab;
	public Color LoadedColor = Color.yellow;
	public Color DepletedColor = Color.grey;
	public GameObject ReloadHintText;

	/// <summary>
	/// Where the cells start. The components are a multiplier for the cell's width and height.
	/// </summary>
	public Vector2 CellStart = new Vector2(-0.5f, 0.5f);

	/// <summary>
	/// What direction the cells will stretch out.The components are a multiplier for the cell's width and height.
	/// </summary>
	public Vector2 CellDirection = Vector2.left;

	public PlayerShoot ShootScript;

	public void UpdateAmmoDisplay()
	{
		foreach (Transform child in transform)
		{
			if (child.gameObject != ReloadHintText)
			{
				Destroy(child.gameObject);
			}
		}

		int index = 0;
		for (; index < ShootScript.ammo; index++)
		{
			SpawnAmmo(index, LoadedColor);
		}
		for (; index < ShootScript.maxAmmo; index++)
		{
			SpawnAmmo(index, DepletedColor);
		}

		ReloadHintText.SetActive(ShootScript.ammo == 0);
	}

	private void SpawnAmmo(int index, Color cellColor)
	{
		GameObject ammo = Instantiate(AmmoPrefab);
		ammo.transform.SetParent(transform);

		// Position the cell correctly
		Vector3 cellPosition = ammo.transform.position;
		float cellWidth = ammo.GetComponent<RectTransform>().rect.width;
		float cellHeight = ammo.GetComponent<RectTransform>().rect.width;

		ammo.transform.position = new Vector3(
			transform.parent.gameObject.GetComponent<RectTransform>().rect.width + (index * CellDirection.x + CellStart.x) * cellWidth,
			(index * CellDirection.y + CellStart.y) * cellHeight,
			cellPosition.z
		);

		// Colorize the cell
		ammo.GetComponent<Image>().color = cellColor;
	}
}