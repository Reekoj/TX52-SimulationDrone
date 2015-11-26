using UnityEngine;
using System.Collections;

public class ClickMovement : MonoBehaviour {

	/*public GameObject drone;
	public Transform target;

	// Use this for initialization
	void Start () {
	
	}

	void Update() {
		if (Input.GetButtonDown("Fire1")) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray))
				target.position = Input.mousePosition;
		}
	}*/

	public GameObject objectToMove;

	Vector3 newPosition;
	float arriveRadius = 4;

	void Start () {
		newPosition = objectToMove.transform.position;
	}
	void Update()
	{
		if (objectToMove != null) {


			if (Input.GetMouseButtonDown (1)) {
					RaycastHit hit;
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					if (Physics.Raycast (ray, out hit)) 
					{
						newPosition = hit.point;
						Debug.Log(newPosition);
						//newPosition = newPosition + new Vector3 (0f, 1f, 0f);
						//objectToMove.transform.position += newPosition;

					}
			}



			/*float distanceToTarget = Vector3.Distance(objectToMove.transform.position, newPosition);
			if (distanceToTarget > arriveRadius-5 && distanceToTarget < arriveRadius+5)
				objectToMove.rigidbody.AddForce(newPosition.normalized * -2);
			else
				objectToMove.rigidbody.AddForce(newPosition.normalized * 5);*/
			
			float distanceToTarget = Vector3.Distance(objectToMove.transform.position, newPosition);

			Vector3 direction = newPosition - objectToMove.GetComponent<Rigidbody>().position;

			if(distanceToTarget < arriveRadius)
				objectToMove.GetComponent<Rigidbody>().AddForce(direction.normalized * -2);
			else
				objectToMove.GetComponent<Rigidbody>().AddForce(direction.normalized * 10);

			//objectToMove.transform.position = Vector3.MoveTowards (transform.position, newPosition, 0.25f);
			//NavMeshAgent agent = objectToMove.GetComponent<NavMeshAgent>();

			//agent.SetDestination(newPosition);
		}
	}
}
