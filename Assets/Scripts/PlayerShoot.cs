using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour {

    public Transform firePoint;
    public int ammo;
    public GameObject Projectile;
	public Text ammocount;


	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
		if (ammo > 0) {
			ammocount.color = Color.white;
			ammocount.text = ammo.ToString ();
		} else {
			ammocount.color = Color.red;
			ammocount.text = "Right click to reload!";
		}
        if (Input.GetMouseButtonDown(0))
        {
            shootBullet();
            ammo -= 1;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Reloading...");
			ammo = 6;
            Debug.Log("Reloaded.");
        }
    }

    void shootBullet()
    {
        if (ammo > 0)
        {
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 myPos = (Vector2)transform.position;
            Vector2 dir = (target - myPos).normalized;

            GameObject bullet = Instantiate(Projectile, firePoint.position, firePoint.rotation);
            BulletScript b = bullet.GetComponent<BulletScript>();
            b.direction = dir;
        }
    }
}
