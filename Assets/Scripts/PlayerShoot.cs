using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public Transform firePoint;
    public int cooldown;
    public GameObject Projectile;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            shootBullet();
        }
	}

    void shootBullet()
    {
        Instantiate(Projectile, firePoint.position, firePoint.rotation);
    }
}
