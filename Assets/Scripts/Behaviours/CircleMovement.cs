using UnityEngine;
using System.Collections;

public class CircleMovement : MonoBehaviour {

	public float speed = 10;
	public float radius = 5;
	private GameObject target;
	
	void OnEnable() {
		target = GameObject.FindGameObjectWithTag ("Goal");
	}

	void Update()
	{	
		float distanceToTarget = Vector3.Distance(target.transform.position, GetComponent<Rigidbody>().position);
		
		if (distanceToTarget > radius-5 && distanceToTarget < radius+5)
			turnAround ();
		else
			findGoodPosition ();
	}

	void findGoodPosition(){
		
		float distanceToTarget = Vector3.Distance(target.transform.position, GetComponent<Rigidbody>().position); // distance to goal
		Vector3 direction = target.transform.position - GetComponent<Rigidbody>().position;
		
		if(distanceToTarget > radius)
			GetComponent<Rigidbody>().AddForce(direction.normalized * speed);
		else if (distanceToTarget < radius)
			GetComponent<Rigidbody>().AddForce(direction.normalized * speed * -1);
	}

	void turnAround(){
		Vector3 direction = target.transform.position - GetComponent<Rigidbody>().position;
		Vector3 up = Vector3.up;
		Vector3 direction2 = Vector3.Cross(direction, up);
		GetComponent<Rigidbody>().AddForce(direction2.normalized * speed);
	}
}
