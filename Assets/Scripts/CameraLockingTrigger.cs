using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockingTrigger : MonoBehaviour
{
    public Vector3 ToPanTo;

    private Camera mainCamera;
    private CameraController cam;

    void Start()
    {
        mainCamera = Camera.main;
        cam = mainCamera.GetComponent<CameraController>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
            cam.PanTo(ToPanTo);
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
			ReturnCameraToPlayer();
    }

	public void ReturnCameraToPlayer()
	{
		cam.toFollow = PlayerMovement.instance;
	}

}
