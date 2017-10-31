using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class with common traits of all enemies
//(Most) Enemies have health, and die when it reaches 0

public class EnemyHealth : AbstractHealth
{
	protected override void OnDeath()
	{
		Destroy(gameObject);
	}
}