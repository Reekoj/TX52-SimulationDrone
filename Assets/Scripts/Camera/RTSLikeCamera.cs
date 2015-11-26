using UnityEngine;
using System.Collections;

public class RTSLikeCamera : MonoBehaviour {
	
	private float distance;
	private float sensitivityDistance;
	private float damping;
	private float minFOV;
	private float maxFOV;
	
	void OnEnable () {
		
		distance = 60;
		sensitivityDistance = 50;
		damping = 5;
		minFOV = 10;
		maxFOV = 150;

		this.transform.position = new Vector3 (-23, 35, -23);
		this.transform.rotation = Quaternion.Euler(45, 45, 0);

		Cursor.visible = true;
		distance = GetComponent<Camera>().fieldOfView;		
	}
	
	void Update () {
		
		float mousePosX = Input.mousePosition.x; 
		float mousePosY = Input.mousePosition.y; 
		float scrollDistance = 5f; 
		float scrollSpeed = 70;
		if (Input.GetMouseButton(0))
		{
			if (mousePosX < scrollDistance) 
			{ 
				transform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime); 
			} 
			
			if (mousePosX >= Screen.width - scrollDistance) 
			{ 
				transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime); 
			}
			
			if (mousePosY < scrollDistance) 
			{ 
				transform.Translate(transform.up * -scrollSpeed * Time.deltaTime); 
			} 
			
			if (mousePosY >= Screen.height - scrollDistance) 
			{ 
				transform.Translate(transform.up * scrollSpeed * Time.deltaTime); 
			}
		}
		scrollDistance -= (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * scrollSpeed * Mathf.Abs(scrollDistance);

		distance -= Input.GetAxis("Mouse ScrollWheel") * sensitivityDistance;
		distance = Mathf.Clamp(distance, minFOV, maxFOV);
		this.GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, distance, Time.deltaTime * damping);		
	}
}
