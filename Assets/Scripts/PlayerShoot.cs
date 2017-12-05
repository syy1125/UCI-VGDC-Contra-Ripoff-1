using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerShoot : MonoBehaviour {

    public Transform firePoint;
    public int ammo;
	public int maxAmmo = 6;
    public GameObject Projectile;
	public int timer = 1;
	private bool reload_now = false;


	// Use this for initialization
	void Start () 
	{
		ammo = maxAmmo;
		AmmoHud.Instance.UpdateAmmoDisplay();
	}

    // Update is called once per frame
	void Update(){
		//if (ammo > 0) {
			//ammocount.color = Color.white;
			//ammocount.text = ammo.ToString ();
		//} else {
			//ammocount.color = Color.red;
			//ammocount.text = "Right click to reload!";
		//}
		if (Input.GetMouseButtonDown(0) && ammo > 0 && reload_now == false)
        {
			shootBullet ();
        }
        if (Input.GetMouseButtonDown(1))
        {
			reload_now = true;
			StartCoroutine(reload_time());
		}
		if (Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
			ammo--;
	        
	        AmmoHud.Instance.UpdateAmmoDisplay();
        }
    }

	private IEnumerator reload_time(){
		yield return new WaitForSeconds(timer);
		ammo = maxAmmo;
		reload_now = false;
		AmmoHud.Instance.UpdateAmmoDisplay();
	}
}
