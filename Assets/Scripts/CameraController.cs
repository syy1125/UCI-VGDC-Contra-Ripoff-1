using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public Vector3 offset;
	public GameObject toFollow;

	void LateUpdate () 
	{
		this.transform.position = toFollow.transform.position + offset;
	}
}
