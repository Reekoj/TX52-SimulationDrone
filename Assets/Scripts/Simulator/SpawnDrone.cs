using UnityEngine;
using System.Collections;

public class SpawnDrone : MonoBehaviour {

	public GameObject drone;
	public GameObject center;
	
	void Start () {


		Instantiate(center, center.transform.position, center.transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
