using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public string inputSteerAxis = "Horizontal";
    public string inputThrotleAxis = "Vertical";

    public float ThrotleInput { get; private set; }
    public float SteerInput { get; private set; }
    public int PlayerNumber { get; set; }

    void Start()
    {
        inputSteerAxis = "Horizontal" + PlayerNumber;
        inputThrotleAxis = "Vertical" + PlayerNumber;
}

    // Update is called once per frame
    void Update()
    {
        SteerInput = Input.GetAxis(inputSteerAxis);
        ThrotleInput = Input.GetAxis(inputThrotleAxis);
    }
}
