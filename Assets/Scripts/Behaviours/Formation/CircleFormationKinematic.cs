using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleFormationKinematic : CircleFormation {
	
	void Update () {
	
		Vector3 direction = new Vector3();
		
		/* Respect distance to goal */		
		Vector3 goalPosition = goal.transform.position;		
		float distanceToGoal = Vector3.Distance(goalPosition, this.transform.position);
				
		if(distanceToGoal > radius)
			direction += goalPosition - this.transform.position;		
		else if (distanceToGoal < radius)
			direction += this.transform.position - goalPosition;	
		
		
		/* Respect same height as goal */
		if(goalPosition.y > this.transform.position.y)
			direction += Vector3.up;		
		else if (goalPosition.y < this.transform.position.y)
			direction += Vector3.down;		
		
		
		/* Respect distance to other drones */
		GameObject nearestDrone = this.getNearestEntity(drones, this.gameObject);
						
		if(nearestDrone != null) {			
			float nearestDroneDistance = Vector3.Distance(nearestDrone.transform.position, this.transform.position);
			
			if(nearestDroneDistance > distanceBetweenDrone)
				direction += (nearestDrone.transform.position - this.transform.position) * 2f;
			else if (nearestDroneDistance < distanceBetweenDrone)
				direction += (nearestDrone.transform.position - this.transform.position) * 2f * -1;
		}
		
			
		/* Move the drone */
		this.GetComponent<Rigidbody>().velocity = direction.normalized * droneSpeed;
	}
}