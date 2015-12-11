using UnityEngine;
using System.Collections;

public class DroneControllerV2 : MonoBehaviour {

    private float m_height;
    public PelicanController m_drone;

    public Transform m_target;

    public float m_xTarget;
    public float m_zTarget;

    public PID m_xPID;
    public PID m_zPID;
    
    public float Speed;

    public float Roll;
    public float Pitch;
    public float Yaw;

    public float Height;

    private bool m_stabilize;

	// Use this for initialization
	void Start () {
        if (m_drone != null)
            m_height = m_drone.transform.position.y;
        else
            m_height = 0;

        Speed = 2.0f;
        Yaw = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_drone != null)
        {
            if(Input.GetButtonDown("Stabilize"))
            {
                m_xTarget = m_drone.transform.position.x;
                m_zTarget = m_drone.transform.position.z;
                m_stabilize = true;
            }
            else if(Input.GetButtonUp("Stabilize"))
            {
                m_stabilize = false;
            }

            m_height += Input.GetAxis("Vertical") * Speed * Time.deltaTime;

            Height = m_height;
            Roll = Input.GetAxis("Roll");
            Pitch = Input.GetAxis("Pitch");
            Yaw += Input.GetAxis("Yaw");

            if (m_target != null)
                m_target.Translate(0, Input.GetAxis("Vertical") * Speed * Time.deltaTime, 0);

            m_drone.SetHeight(m_height);
            m_drone.SetYaw(Yaw);

            if(m_stabilize)
            {
                float xx = m_xPID.Update(m_xTarget, m_drone.transform.position.x, Time.deltaTime);
                float zz = m_zPID.Update(m_zTarget, m_drone.transform.position.z, Time.deltaTime);

                m_drone.SetDirection(-xx, zz);
            }
            else
            {
                m_drone.SetDirection(Roll, Pitch);
            }
        }
	}
}
