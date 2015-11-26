using UnityEngine;
using System.Collections;

public class HumanController : MonoBehaviour {

	public int speed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Z)) {
			this.transform.position += new Vector3(1,0,0)*speed*Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.S)) {
			this.transform.position += new Vector3(-1,0,0)*speed*Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.Q)) {
			this.transform.position += new Vector3(0,0,1)*speed*Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.D)) {
			this.transform.position += new Vector3(0,0,-1)*speed*Time.deltaTime;
		}
	}
}
