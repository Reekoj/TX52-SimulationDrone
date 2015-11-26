using UnityEngine;
using System.Collections;

public class Wandering : MonoBehaviour {
	
	public float rotationSpeed;
	public float movementSpeed;
	public float rotationTime;
	
	void Start()
	{
		Invoke("ChangeRotation",rotationTime);
	}
	
	void ChangeRotation()
	{
		float randomValue = Random.Range (0.0f, 1.0f);
		if(randomValue > 0.5f)
		{
			rotationSpeed = -rotationSpeed;
		}
		Invoke("ChangeRotation",rotationTime);
	}
	
	
	void Update() {
		
		transform.Rotate (new Vector3 (0, rotationSpeed * Time.deltaTime, 0));
		transform.position += transform.forward*movementSpeed*Time.deltaTime;
		
	}
}