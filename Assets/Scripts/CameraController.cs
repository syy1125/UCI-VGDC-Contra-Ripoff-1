using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

	public Vector3 offset;
	public GameObject toFollow;
    public float panSpeed;

    private float InversePanSpeed;
    private bool isPanning;
         
    void Start()
    {
        InversePanSpeed = 1 / panSpeed;
        isPanning = false;
    }

    void LateUpdate()
    {
        if (!isPanning && toFollow != null)
        {
            this.transform.position = toFollow.transform.position + offset;
        }
    }

    public void PanTo(Vector3 pos)
    {  
        StartCoroutine(Pan(pos+offset));
        toFollow = null;
    }

    private IEnumerator Pan(Vector3 pos)
    {
		PlayerMovement.instance.GetComponent<PlayerMovement> ().removePlayerControl ();
		PlayerMovement.instance.GetComponent<PlayerMovement> ().stopPlayer ();
        isPanning = true;
        float sqrRemainingDistance = (transform.position - pos).sqrMagnitude;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, pos, InversePanSpeed * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - pos).sqrMagnitude;
            yield return null;
        }
        isPanning = false;
		PlayerMovement.instance.GetComponent<PlayerMovement> ().returnPlayerControl ();

    }

}
