using UnityEngine;
using System.Collections;

public class FreeCenteredCamera : MonoBehaviour {
	
	public Transform target;
	public bool click;
	
	public float distance = 5.0f;
	
	private float xSpeed = 250.0f;
	private float ySpeed = 120.0f;

	private float zoomRate = 20f;

	private float x = 0.0f;
	private float y = 0.0f;
	
	
	void OnEnabled () {
		var angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		
		//Screen.showCursor = false;
	}
	
	void LateUpdate () {
		if(!target)
			return;
		
		// If either mouse buttons are down, let them govern camera position
		if (click==false || (Input.GetMouseButton(0) || Input.GetMouseButton(1)))
		{
			x += (float) (Input.GetAxis("Mouse X") * xSpeed * 0.02);
			y -= (float) (Input.GetAxis("Mouse Y") * ySpeed * 0.02);
		}
		
		distance -= (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
				
		Quaternion rotation = Quaternion.Euler(y, x, 0);
		Vector3 position = target.position - (rotation * Vector3.forward * distance);
		
		transform.rotation = rotation;
		transform.position = position;
	}
}
