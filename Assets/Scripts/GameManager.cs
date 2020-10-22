using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public CarController[] carPrefabs;

    public float NumberOfPlayers { get; set; }
    public InputController[] InputControllers { get; private set; }
    private TrackController trackController;
    void Awake()
    {
        Instance = this;
        NumberOfPlayers = carPrefabs.Length;
        InputControllers = new InputController[carPrefabs.Length];
        for (int i = 0; i < carPrefabs.Length; i++)
        {
            InputController inputController = gameObject.AddComponent<InputController>();
            inputController.PlayerNumber = i;
            InputControllers[i] = inputController;
        }

        trackController = GetComponentInChildren<TrackController>();
    }

    private void Start()
    {
        for (int i = 0; i < carPrefabs.Length; i++)
        {
            CarController carController = Instantiate(carPrefabs[i], trackController.startPoint.position + trackController.startPoint.right * 4 * i, trackController.startPoint.rotation);
            carController.PlayerNumber = i;
        }
    }

    void Update()
    {

    }
}
