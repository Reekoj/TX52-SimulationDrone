using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractFormation : MonoBehaviour {
	
	protected GameObject getNearestEntity(List<GameObject> entities, GameObject reference) {
	
		GameObject nearestEntity = null;
		float nearestEntityDistance = Mathf.Infinity;
		
		foreach (GameObject entity in entities) {
			if(entity != reference) {
				float distanceToEntity = Vector3.Distance(entity.transform.position, reference.transform.position);
				
				if(distanceToEntity < nearestEntityDistance) {
					nearestEntity = entity;
					nearestEntityDistance = distanceToEntity;
				}
			}				
		}
		
		return nearestEntity;
	}
}
