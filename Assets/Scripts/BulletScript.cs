using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour 
{
    GameObject Player;
    public float Speed;
	public Vector2 direction;

    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null)
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
		rb.velocity = direction.normalized*Speed;//.normalized.Scale(new Vector2(Speed, Speed));

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Note: Eventually need ways to check if enemy is a boss
            ScoreHud.Instance.SetScore(0);  //How many points should the player get for killing an enemy?
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
