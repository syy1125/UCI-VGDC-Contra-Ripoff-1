using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls makes an enemy that only moves towards a player.

public class EnemyMoveToPlayer : Enemy {
    public float speed = 0.01f;
    private float playerPositionX;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        playerPositionX = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        transform.position = Vector2.MoveTowards(transform.position,
            new Vector2(playerPositionX, transform.position.y), speed);
    }
}
