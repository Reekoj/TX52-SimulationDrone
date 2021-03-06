using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleFormationSteering : CircleFormation {
	
	void OnEnable () {
		
		drones.Clear ();
		
		goal = GameObject.FindGameObjectWithTag("Goal");
		
		drones.AddRange( GameObject.FindGameObjectsWithTag("Drone") );
		
		radius = drones.Count * 1.25f;
		
		float squareRadius = radius * radius;
		
		angleBetweenDrone = 360 / drones.Count;
		distanceBetweenDrone = Mathf.Sqrt(squareRadius + squareRadius - 2*squareRadius*Mathf.Cos(angleBetweenDrone));
	}
	
	void Update () {
		
		Vector3 direction = new Vector3();
		
		/* Respect distance to goal */		
		Vector3 goalPosition = goal.transform.position;		
		float distanceToGoal = Vector3.Distance(goalPosition, this.transform.position);
		
		if(distanceToGoal > radius)
			direction += (goalPosition - this.transform.position).normalized * 2;		
		else if (distanceToGoal < radius)
			direction += (this.transform.position - goalPosition).normalized * 2;
		
		
		/* Respect same height as goal */
		if(goalPosition.y > this.transform.position.y + 0.5f)
			direction += Vector3.up * 3;
		else if (goalPosition.y < this.transform.position.y - 0.5f)
			direction += Vector3.down * 3;		
		
		
		/* Respect distance to other drones */
		GameObject nearestDrone = this.getNearestEntity(drones, this.gameObject);
		
		if(nearestDrone != null) {			
			float nearestDroneDistance = Vector3.Distance(nearestDrone.transform.position, this.transform.position);
			
			if(nearestDroneDistance > distanceBetweenDrone)
				direction += (nearestDrone.transform.position - this.transform.position).normalized * 2;
			else if (nearestDroneDistance < distanceBetweenDrone)
				direction += (this.transform.position - nearestDrone.transform.position).normalized;
		}
		
		
		/* Move the drone */
		this.GetComponent<Rigidbody>().AddForce(direction.normalized * droneSpeed);
	}
}
