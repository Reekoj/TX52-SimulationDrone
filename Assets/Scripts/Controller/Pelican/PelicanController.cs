using UnityEngine;
using System.Collections;

public class PelicanController : MonoBehaviour {

    public PID m_yPID;
    public PID m_yawPID;
    public PID m_pitchPID;
    public PID m_rollPID;

    public bool m_debugging;
    private System.Collections.Generic.LinkedList<float> m_debugTargetHeight;
    private System.Collections.Generic.LinkedList<float> m_debugCurrentHeight;

    private float m_heightTarget = 0;
    private float m_pitchTarget = 0;
    private float m_rollTarget = 0;
    private float m_yawTarget = 0;

	private PelicanEngine engine1;
	private PelicanEngine engine2;
	private PelicanEngine engine3;
	private PelicanEngine engine4;

	private GameObject propeller1;
	private GameObject propeller2;
	private GameObject propeller3;
	private GameObject propeller4;

	private LineRenderer lrEngine1;
	private LineRenderer lrEngine2;
	private LineRenderer lrEngine3;
	private LineRenderer lrEngine4;

	// Use this for initialization
	void Start () {

		// Create engine
		engine1 = createEngine(1,4);
		engine2 = createEngine(2,4);
		engine3 = createEngine(3,4);
		engine4 = createEngine(4,4);

		// Init propellers
		propeller1 = GameObject.Find("PrefabDrone/quadfinal/Cone.002");
		propeller2 = GameObject.Find("PrefabDrone/quadfinal/Cone.009");
		propeller3 = GameObject.Find("PrefabDrone/quadfinal/Cone.005");
		propeller4 = GameObject.Find("PrefabDrone/quadfinal/Cone.013");

        lrEngine1 = propeller1.GetComponent<LineRenderer>();
        lrEngine2 = propeller2.GetComponent<LineRenderer>();
        lrEngine3 = propeller3.GetComponent<LineRenderer>();
        lrEngine4 = propeller4.GetComponent<LineRenderer>();

		reset();
	}

	// Reset controller
	void reset() {
		engine1.CutEngine();
		engine2.CutEngine();
		engine3.CutEngine();
		engine4.CutEngine();

		transform.position = new Vector3(0,2,0);
		transform.rotation = new Quaternion();

        m_debugCurrentHeight = new System.Collections.Generic.LinkedList<float>();
        m_debugTargetHeight = new System.Collections.Generic.LinkedList<float>();
	}

    public void SetDirection(float _dx, float _dy)
    {
        SetDirection(new Vector2(_dx, _dy));
    }

    public void SetDirection(Vector2 _direction)
    {
        m_pitchTarget = Mathf.Clamp(_direction.y, -1, 1) * 45;
        m_rollTarget = Mathf.Clamp(_direction.x, -1, 1) * 45;
    }

    public void SetHeight(float _height)
    {
        m_heightTarget = _height;
    }

    public void SetYaw(float _yaw)
    {
        m_yawTarget = _yaw;
    }

    private float ClampAngle(float _angle)
    {
        if (_angle < 360 - _angle)
        {
            return _angle;
        }
        else
        {
            return _angle - 360;
        }
    }

    private float ComputeYawError(float _target, float _value)
    {
        // If both angle same sign, no problems
        return Mathf.DeltaAngle(_value, _target);
    }

	// Update is called once per frame
	void Update () {

        // Update height PID
		float yy = m_yPID.Update(m_heightTarget, transform.position.y, Time.deltaTime);

        float angle = 0;
        
        // Update pitch PID
        angle = ClampAngle(transform.rotation.eulerAngles.x);
        float pitch = m_pitchPID.Update(m_pitchTarget, angle, Time.deltaTime);

        // Update roll PID
        angle = ClampAngle(transform.rotation.eulerAngles.z);
        float roll = m_rollPID.Update(m_rollTarget, angle, Time.deltaTime);

        // Update yaw PID
        float eyaw = ComputeYawError(m_yawTarget, transform.rotation.eulerAngles.y);
        float yaw = m_yawPID.Update(eyaw, Time.deltaTime);

        // Set new throttles
        engine1.SetThrottle(yy + roll - yaw);
		engine2.SetThrottle(yy - pitch + yaw);
		engine3.SetThrottle(yy - roll - yaw);
        engine4.SetThrottle(yy + pitch + yaw);

		lrEngine1.SetPosition (1, engine1.GetForce());
		lrEngine2.SetPosition (1, engine2.GetForce());
		lrEngine3.SetPosition (1, engine3.GetForce());
		lrEngine4.SetPosition (1, engine4.GetForce());

        if(m_debugging)
        {
            m_debugTargetHeight.AddLast(m_heightTarget);
            m_debugCurrentHeight.AddLast(transform.position.y);
        }
	}

	// Show info
	private void OnGUI ()
	{
		if (GUI.Button (new Rect (10, 10, 100, 20), "Restart")) {
			reset ();
		}

		GUI.Label (new Rect (10, 20, Screen.width, Screen.height),
		           "\n\nDrone" +
		           "\n\tposition: " + transform.position.ToString() +
		           "\n\tup: " + transform.up.ToString() +
		           "\n\tangles: " + transform.eulerAngles.ToString() + 
		           "\n\nLast errors" +
		           "\n\tY PID: " + m_yPID.LastError +
		           "\n\tRoll PID: " + m_rollPID.LastError +
		           "\n\tPitch PID: " + m_pitchPID.LastError + 
		           "\n\tYaw PID: " + m_yawPID.LastError +
		           "\n\nEngines"+
		           "\n\tengine1: " + engine1.GetThrottle() +
		           "\n\tengine2: " + engine2.GetThrottle() +
		           "\n\tengine3: " + engine3.GetThrottle() +
		           "\n\tengine4: " + engine4.GetThrottle()
		           );

	}

	void FixedUpdate() {
		Vector3 position1 = propeller1.transform.position;
		Vector3 position2 = propeller2.transform.position;
		Vector3 position3 = propeller3.transform.position;
		Vector3 position4 = propeller4.transform.position;

		GetComponent<Rigidbody>().AddForceAtPosition(engine1.GetForce(), position1);
		GetComponent<Rigidbody>().AddForceAtPosition(engine3.GetForce(), position3);
		GetComponent<Rigidbody>().AddForceAtPosition(engine2.GetForce(), position2);
		GetComponent<Rigidbody>().AddForceAtPosition(engine4.GetForce(), position4);

		GetComponent<Rigidbody>().AddTorque(engine1.GetTorque());
		GetComponent<Rigidbody>().AddTorque(engine2.GetTorque());
		GetComponent<Rigidbody>().AddTorque(engine3.GetTorque());
		GetComponent<Rigidbody>().AddTorque(engine4.GetTorque());

		propeller1.transform.Rotate(Vector3.right, 100 * engine1.throttle / 100f, Space.Self);
		propeller2.transform.Rotate(Vector3.right, -100 * engine2.throttle / 100f, Space.Self);
		propeller3.transform.Rotate(Vector3.right, 100 * engine3.throttle / 100f, Space.Self);
		propeller4.transform.Rotate(Vector3.right, -100 * engine4.throttle / 100f, Space.Self);
	}

	private PelicanEngine createEngine(int type, float maxPower) {
		PelicanEngine engine;
		engine = gameObject.AddComponent<PelicanEngine>();
		engine.type = type;
		engine.maxPower = maxPower;
		return engine;
	}
}
