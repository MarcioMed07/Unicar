using UnityEngine;

public class CarCam : MonoBehaviour
{
    Transform rootNode;
    Camera carCam;
    Transform car;
    CarController carController;
    Rigidbody carPhysics;

    [Tooltip("If car speed is below this value, then the camera will default to looking forwards.")]
    public float rotationThreshold = 1f;

    [Tooltip("How closely the camera follows the car's position. The lower the value, the more the camera will lag behind.")]
    public float cameraStickiness = 10.0f;

    [Tooltip("How closely the camera matches the car's velocity vector. The lower the value, the smoother the camera rotations, but too much results in not being able to see where you're going.")]
    public float cameraRotationSpeed = 5.0f;

    void Awake()
    {
        carCam = GetComponentInChildren<Camera>();
        rootNode = GetComponent<Transform>();
        car = rootNode.parent.GetComponent<Transform>();
        carPhysics = car.GetComponent<Rigidbody>();
        carController = rootNode.parent.GetComponent<CarController>();
    }

    void Start()
    {
        // Detach the camera so that it can move freely on its own.
        carCam.enabled = true;
        float new_width = 1 / GameManager.Instance.NumberOfPlayers;
        if (GameManager.Instance.NumberOfPlayers > 0)
        {
            carCam.tag = "MainCamera";
            carCam.rect = new Rect(new Vector2(carController.PlayerNumber * new_width, 0), new Vector2(new_width, 1));

        }
        rootNode.parent = null;
    }

    void FixedUpdate()
    {
        Quaternion look;
        if (!car)
        {
            Destroy(gameObject);
        }
        else
        {

            // Moves the camera to match the car's position.
            rootNode.position = Vector3.Lerp(rootNode.position, car.position, cameraStickiness * Time.fixedDeltaTime);

            // If the car isn't moving, default to looking forwards. Prevents camera from freaking out with a zero velocity getting put into a Quaternion.LookRotation
            if (carPhysics.velocity.magnitude < rotationThreshold)
                look = Quaternion.LookRotation(car.forward);
            else
                look = Quaternion.LookRotation(carPhysics.velocity.normalized);

            // Rotate the camera towards the velocity vector.
            look = Quaternion.Slerp(rootNode.rotation, look, cameraRotationSpeed * Time.fixedDeltaTime);
            rootNode.rotation = look;
        }
    }
}