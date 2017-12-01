/// <summary>
/// Class with common traits of all enemies
/// (Most) Enemies have health, and die when it reaches 0
/// </summary>
public class EnemyHealth : AbstractHealth
{
	protected override void OnDeath()
	{
		ScoreHud.Instance.SetScore (10);
		Destroy (this.gameObject);
	}
}