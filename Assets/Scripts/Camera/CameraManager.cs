using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	private int cameraSelected = 0;
	RTSLikeCamera rtsCamera;
	FreeCenteredCamera freeCenteredCamera;

	// Use this for initialization
	void Start () {
		rtsCamera = this.GetComponent<RTSLikeCamera>();
		freeCenteredCamera = this.GetComponent<FreeCenteredCamera> ();
	}
	
	// Update is called once per frame
	void Update () {
		cameraController ();
	}

	void cameraController()
	{
		if(Input.GetKeyDown(KeyCode.V))
		{
			if(cameraSelected == 0)
			{
				rtsCamera.enabled = false;
				Debug.Log("Camera free centered activée");
				freeCenteredCamera.enabled = true;
				cameraSelected = 1;
			}
			else
			{
				freeCenteredCamera.enabled = false;
				Debug.Log("RTS camera activée");
				rtsCamera.enabled = true;
				cameraSelected = 0;
			}
		}
	}
}
