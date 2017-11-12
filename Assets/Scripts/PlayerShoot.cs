using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public Transform firePoint;
    public float cooldown;
    public GameObject Projectile;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
        {
            shootBullet();
        }
	}

    void shootBullet()
    {
		Debug.Log ("trying to shoot");
		Vector2 target = Camera.main.ScreenToWorldPoint (new Vector2 (Input.mousePosition.x, Input.mousePosition.y));
		Vector2 myPos = (Vector2)transform.position;
		Vector2 dir = (target-myPos).normalized;
		Debug.Log (target);
		Debug.Log (myPos);
		Debug.Log (dir);
		GameObject bullet = Instantiate(Projectile, firePoint.position, firePoint.rotation);
		BulletScript b = bullet.GetComponent<BulletScript> ();
		b.Speed = 1;
		b.direction = dir;
    }
}
