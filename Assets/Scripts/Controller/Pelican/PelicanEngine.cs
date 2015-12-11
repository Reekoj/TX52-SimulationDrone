using UnityEngine;
using System.Collections;
using System;

public class PelicanEngine : MonoBehaviour
{
	public float maxPower;
	public float throttle;
	public int type;

	private Vector3 force;
	private Vector3 torque;
		
	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void FixedUpdate () {		

	}
	
	public void IncreaseThrottle () {
		if (throttle < 100) {
			throttle=throttle+1f;
			SetThrottle(throttle);
		}
	}
	
	public void DecreaseThrottle () {
		if (throttle > 0) {
			throttle=throttle-1f;
			SetThrottle(throttle);
		}	
	}
	
	public void CutEngine(){
		throttle = 0;
	} 

	public void SetThrottle(float value){
		throttle = Mathf.Clamp(value,0,100);
	}
	
	public float GetThrottle () {
		return throttle;
	}

	public Vector3 GetTorque() {
        if (type == 1 || type == 3)
            torque = transform.up * (maxPower * (throttle / 100) * -1);
        else
            torque = transform.up * (maxPower * (throttle / 100));

        return torque;
	}

	public Vector3 GetForce() {
        force = transform.up.normalized * (maxPower * (throttle / 100.0f));
		return force;
	}
}
