using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CarController : MonoBehaviour
{

    [Header("Car Settings")]
    public Transform centerOfMass;
    public float engineTorque;
    public float steerAngle;

    [Header("Read onlys")]
    public int PlayerNumber;

    private Wheel[] wheels;
    Rigidbody rigidBody;

    float Steer, Throtle;

    void Awake()
    {

        wheels = GetComponentsInChildren<Wheel>();
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.centerOfMass = centerOfMass.localPosition;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Steer = GameManager.Instance.InputControllers[PlayerNumber].SteerInput;
        Throtle = GameManager.Instance.InputControllers[PlayerNumber].ThrotleInput;
        Move(Throtle, Steer);


    }

    private void Update()
    {

    }

    float TorquePerWheel()
    {
        float torqueMultiplier = (rigidBody.velocity.magnitude / 100);
        float clampedTorque = Mathf.Clamp(engineTorque / torqueMultiplier, 10, 8000);
        float torquePerWheel = clampedTorque / wheels.Where(w => w.traction).Count();
        return torquePerWheel;
    }


    public void Move(float Throtle, float Steer)
    {
        float curTorque = TorquePerWheel();
        foreach (var wheel in wheels)
        {
            wheel.SteerAngle = Steer * steerAngle;
            wheel.Torque = Throtle * curTorque;
        }
    }
}