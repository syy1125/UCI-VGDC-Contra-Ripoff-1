using UnityEngine;

public class ProjectileTest : MonoBehaviour
{
	public GameObject Player;
	public float Speed;

	// Use this for initialization
	void Start()
	{
		if (Player != null)
		{
			Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

			rb.velocity = (Player.transform.position - gameObject.transform.position) * Speed;
		}
	}
}