using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class CircleFormation : AbstractFormation {
	
	protected GameObject goal;	
	public float droneSpeed = 10;
	
	protected float radius;	
	protected float angleBetweenDrone;
	protected float distanceBetweenDrone;
	
	protected List<GameObject> drones = new List<GameObject>();
}
