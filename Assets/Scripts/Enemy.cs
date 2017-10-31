using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class with common traits of all enemies
//(Most) Enemies have health, and die when it reaches 0

public class Enemy : MonoBehaviour {
    public int baseHealth = 1;
    private int _health;

	public int Health
	{
		get { return _health; }
		set { _health = value; }
	}

	// Use this for initialization
	void Start () {
        Health = baseHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (Health == 0)
            die();
	}

    void die()
    {
        Destroy(this.gameObject);
    }
}
