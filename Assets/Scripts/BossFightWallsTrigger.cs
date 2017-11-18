using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightWallsTrigger : MonoBehaviour {

    public GameObject WallToCreate;
    public GameObject Boss;
    public Vector3 RightWallPosition;
    public Vector3 LeftWallPosition;
	public Vector3 BossPosition;

    private GameObject leftWall;
    private GameObject rightWall;
    private bool inBossFight;
	private GameObject BossInstance;

	void Start ()
    {
        inBossFight = false;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
			StartCoroutine(BossFight());
        }
    }

	private IEnumerator BossFight()
	{
		leftWall = Instantiate(WallToCreate, LeftWallPosition, Quaternion.identity);
		rightWall = Instantiate(WallToCreate, RightWallPosition, Quaternion.identity);
		inBossFight = true;
		BossInstance = Instantiate(Boss, BossPosition, Quaternion.identity); 
		while(BossInstance != null)
		{
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		GetComponent<CameraLockingTrigger>().ReturnCameraToPlayer();
	}
}
