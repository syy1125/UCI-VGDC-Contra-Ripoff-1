using UnityEngine;

public class HealthPickUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log(gameObject.name + ": collided with " + collision.gameObject.name);

		if (collision.collider.name.Equals("Player"))
		{
			PlayerHealth temp = PlayerMovement.instance.gameObject.GetComponent<PlayerHealth>();
			temp.Heal(this.gameObject, temp.MaxHealth - temp.getCurrentHealth());
			gameObject.SetActive(false);
		}
	}

	/*private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log(gameObject.name + ": collided with " + collision.gameObject.name);

		/**if (collision.GetComponent<Collider>().name.Equals("Player"))
		{
			gameObject.SetActive(false);
		}

		if (collision.gameObject.name.Equals("Player"))
		{
			gameObject.SetActive(false);
		}
	}*/
}
