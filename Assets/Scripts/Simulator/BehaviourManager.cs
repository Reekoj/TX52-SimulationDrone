using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BehaviourManager : MonoBehaviour {

	List<GameObject> drones;
	public GameObject prefabDrone;
	private string behaviour;
	
	void Start () {		
		drones = new List<GameObject> ();
		for(int i=-20; i <= 20; i+=10) {
			for(int y=-20; y <= 20; y+=10) {
				Quaternion orientation = Quaternion.AngleAxis(Random.Range(-180f, 180f), Vector3.up);
				Instantiate(prefabDrone, prefabDrone.transform.position + new Vector3(i,Random.Range(-1,1),y), orientation);
			}
		}		
		drones.AddRange( GameObject.FindGameObjectsWithTag("Drone") );
	}

	void Update()
	{
		behaviourController ();
	}

	void switchBehaviour()
	{
		switch(this.behaviour)
		{
		case "Wandering":
			foreach(GameObject drone in drones)
			{
				Wandering wandering = drone.GetComponent<Wandering>();
				wandering.enabled =! wandering.enabled;
			}
		break;

		case "CircleFormation" :
			foreach(GameObject drone in drones)
			{
				CircleFormation circleFormation = drone.GetComponent<CircleFormation>();
				circleFormation.enabled =! circleFormation.enabled;
			}
		break;

		case "SphereFormation" :
			foreach(GameObject drone in drones)
			{
				SphereFormation sphereFormation = drone.GetComponent<SphereFormation>();
				sphereFormation.enabled =! sphereFormation.enabled;
			}
		break;

		case "Boid" :
			foreach(GameObject drone in drones)
			{
				Boid boid = drone.GetComponent<Boid>();
				boid.enabled =! boid.enabled;
			}
		break;
			
		case "CircleMovement" :
			foreach(GameObject drone in drones)
			{
				CircleMovement circleMvt = drone.GetComponent<CircleMovement>();
				circleMvt.enabled =! circleMvt.enabled;
			}
			break;

		}
	}

	void behaviourController()
	{
		if(Input.GetKeyDown(KeyCode.F1))
		{
			disableAllScript();
			this.behaviour = "Wandering";
			Debug.Log("Wandering lancé");
			switchBehaviour();
		}
		else if(Input.GetKeyDown(KeyCode.F2))
		{
			disableAllScript();
			this.behaviour = "CircleFormation"; 
			Debug.Log("CircleFormation lancé");
			switchBehaviour();
		}
		else if(Input.GetKeyDown(KeyCode.F3))
		{
			disableAllScript();
			this.behaviour = "SphereFormation"; 
			Debug.Log("SphereFormation lancé");
			switchBehaviour();
		}
		else if(Input.GetKeyDown(KeyCode.F4))
		{
			disableAllScript();
			this.behaviour = "Boid"; 
			Debug.Log("Boid lancé");
			switchBehaviour();
		}
		else if(Input.GetKeyDown(KeyCode.F5))
		{
			disableAllScript();
			this.behaviour = "CircleMovement"; 
			Debug.Log("Circle Movement lancé");
			switchBehaviour();
		}
	}

	void disableAllScript()
	{
		foreach(GameObject drone in drones)
		{
			Wandering wandering 			= drone.GetComponent<Wandering>();
			CircleFormation circleFormation = drone.GetComponent<CircleFormation>();
			SphereFormation sphereFormation = drone.GetComponent<SphereFormation>();
			Boid boid 						= drone.GetComponent<Boid>();
			CircleMovement circle			= drone.GetComponent<CircleMovement>();
			
			if(wandering.enabled)
				wandering.enabled = false;
			if(circleFormation.enabled)
				circleFormation.enabled = false;
			if(sphereFormation.enabled)
				sphereFormation.enabled = false;
			if(boid.enabled)
				boid.enabled = false;
			if(circle.enabled)
				circle.enabled = false;

		}
	}
	
}