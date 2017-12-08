using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightWallsTrigger : MonoBehaviour {

    public GameObject WallToCreate;
    public GameObject Boss;
	public Vector3 LeftWallPosition;
    public Vector3 RightWallPosition;
	public Vector3 BossPosition;
	public Vector3 ToPanTo;

	private Camera mainCamera;
	private CameraController cam;

    private GameObject leftWall;
    private GameObject rightWall;
	private GameObject BossInstance;
	private bool isDone;

	void Start()
	{
		mainCamera = Camera.main;
		cam = mainCamera.GetComponent<CameraController>();
		isDone = false;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player") && !isDone)
        {
			Camera.main.orthographicSize = 3;
			cam.PanTo(ToPanTo);
			StartCoroutine(BossFight());
        }
    }

	private IEnumerator BossFight()
	{
		leftWall = Instantiate(WallToCreate, LeftWallPosition, Quaternion.identity);
		rightWall = Instantiate(WallToCreate, RightWallPosition, Quaternion.identity);
		yield return new WaitForSeconds (Camera.main.GetComponent<CameraController> ().panSpeed);
		BossInstance = Instantiate(Boss, BossPosition, Quaternion.identity);
		
		yield return new WaitWhile(() => BossInstance != null);
		yield return new WaitForSeconds(1f);
		Camera.main.orthographicSize = 2;
		cam.PanTo (PlayerMovement.instance.gameObject.transform.position);
		cam.toFollow = PlayerMovement.instance;
		Destroy(leftWall);
		Destroy(rightWall);
		isDone = true;
	}
}
