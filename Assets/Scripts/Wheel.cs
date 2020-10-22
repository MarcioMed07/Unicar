using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool steer;
    public bool invertSteer;
    public bool traction;
    public float brakeTorque;

    public float SteerAngle {get; set;}
    public float Torque;
    public float RPM;

    private WheelCollider wheelCollider;
    private Transform wheelTransform;

    void Start()
    {
        wheelCollider = GetComponentInChildren<WheelCollider>();
        wheelTransform = GetComponentInChildren<MeshRenderer>().GetComponent<Transform>();
        wheelCollider.ConfigureVehicleSubsteps(5.0f, 5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion quat);
        wheelTransform.position = pos;
        wheelTransform.rotation = quat;
    }

    private void FixedUpdate()
    {
        RPM = wheelCollider.rpm;
        if(steer)
        {
            wheelCollider.steerAngle = SteerAngle * (invertSteer ? -1 : 1);
        }
        if (Mathf.Sign(RPM) != Mathf.Sign(Torque) && RPM != 0 && Torque != 0)
        {
            wheelCollider.brakeTorque = brakeTorque;
        }
        else 
        {
            wheelCollider.brakeTorque = 0;
            if (traction)
            {
                wheelCollider.motorTorque = Torque;
            }
        }
    }
}
