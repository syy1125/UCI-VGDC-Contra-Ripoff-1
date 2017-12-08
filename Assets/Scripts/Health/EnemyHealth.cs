/// <summary>
/// Class with common traits of all enemies
/// (Most) Enemies have health, and die when it reaches 0
/// </summary>
public class EnemyHealth : AbstractHealth
{
	public float KnockbackResistance = 1;

	protected override void OnDeath()
	{
		ScoreHud.Instance.SetScore (10);
		Destroy (this.gameObject);
	}
}