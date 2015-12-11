using UnityEngine;
using System.Collections;

public class DroneControllerForceTest : MonoBehaviour {

    public float heliceSpeed1 = 1.0f;
    public float heliceSpeed2 = 1.0f;
    public float heliceSpeed3 = 1.0f;
    public float heliceSpeed4 = 1.0f;

    public float throttleUp = 1.0f;
    public float altitudeMin = 5;

    public Transform heliceFront1;
    public Transform heliceFront2;
    public Transform heliceBack1;
    public Transform heliceBack2;

    private Rigidbody mainBody;

    ///////// A ETUDIER PLUS TARD /////////
    // faire tourner les hélices : 
    //		if (name.Equals ("One") || name.Equals ("Two")) {
    //			helice.transform.rotation = helice.transform.rotation* Quaternion.Euler((vitesse), 0, 0);	
    //		}else{
    //			helice.transform.rotation = helice.transform.rotation* Quaternion.Euler(-(vitesse), 0, 0);	
    //		}
    // Recuperer l'altitude : 
    // penser à Collider.Raycast pour savoir ce qu'il y a en dessous du drone
    ////////////////////////////////////////


    // Use this for initialization
    void Start () {
        mainBody = GetComponent<Rigidbody>();

        //foreach (Transform child in GetComponentsInChildren<Transform>())
        //{
        //    //Debug.Log(child.name);
        //    if (child.name == "Cone.002") heliceFront1 = child;
        //    if (child.name == "Cone.015") heliceFront2 = child;
        //    if (child.name == "Cone.010") heliceBack1 = child;
        //    if (child.name == "Cone.013") heliceBack2 = child;
        //}

        Debug.Log(heliceFront1.position.ToString());
        Debug.Log(heliceFront2.position.ToString());
        Debug.Log(heliceBack1.position.ToString());
        Debug.Log(heliceBack2.position.ToString());

        // 2 13 5 9 
        //heliceFront1 = this.transform.Find("Cone.002").gameObject;
        //heliceFront2 = this.transform.Find("Cone.015").gameObject;
        //heliceBack1 = this.transform.Find("Cone.010").gameObject;
        //heliceBack2 = this.transform.Find("Cone.013").gameObject;
    }

    // Update is called once per frame
    //void Update () {
    //	if(Input.GetKey(KeyCode.Z)) {
    //		this.transform.position += new Vector3(1,0,0)* heliceSpeed1 * Time.deltaTime;
    //	}
    //	if(Input.GetKey(KeyCode.S)) {
    //		this.transform.position += new Vector3(-1,0,0)* heliceSpeed1 * Time.deltaTime;
    //	}
    //	if(Input.GetKey(KeyCode.Q)) {
    //		this.transform.position += new Vector3(0,0,1)* heliceSpeed1 * Time.deltaTime;
    //	}
    //	if(Input.GetKey(KeyCode.D)) {
    //		this.transform.position += new Vector3(0,0,-1)* heliceSpeed1 * Time.deltaTime;
    //	}
    //}

    void Update()
    {
        droneMovement();
    }

    void upLift()
    {

    }

    void downFall ()
    {

    }

    void droneMovement()
    {
        //Vector3 impulseForce = transform.forward * Input.GetAxis("Vertical") * 1 / 240;
        //float torqueForce = Input.GetAxis("Horizontal") * 1 / 15;

        //mainBody.AddForce(impulseForce * moveSpeed, ForceMode.Impulse);
        //mainBody.AddTorque(torqueForce * rotateSpeed, 0.0f, 0.0f);

        if (Input.GetKey(KeyCode.Z)) {
            mainBody.AddForceAtPosition(new Vector3(0, heliceSpeed1, 0), heliceFront1.position, ForceMode.Force);
            mainBody.AddForceAtPosition(new Vector3(0, heliceSpeed1, 0), heliceFront2.position, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.S))
        {
            mainBody.AddForceAtPosition(new Vector3(0, heliceSpeed1, 0), heliceBack1.position, ForceMode.Force);
            mainBody.AddForceAtPosition(new Vector3(0, heliceSpeed1, 0), heliceBack2.position, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            mainBody.AddForceAtPosition(new Vector3(0, heliceSpeed1, 0), heliceFront1.position, ForceMode.Force);
            mainBody.AddForceAtPosition(new Vector3(0, heliceSpeed1, 0), heliceBack1.position, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            mainBody.AddForceAtPosition(new Vector3(0, heliceSpeed1, 0), heliceFront2.position, ForceMode.Force);
            mainBody.AddForceAtPosition(new Vector3(0, heliceSpeed1, 0), heliceBack2.position, ForceMode.Force);
        }

        mainBody.AddRelativeForce(0, 9.81f, 0, ForceMode.Force);
    }
}
