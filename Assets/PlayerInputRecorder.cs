using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PlayerInputRecorder : MonoBehaviour
{
    private CarController carController;

    private List<ICommand> recorded = new List<ICommand>();
    private float recordingStartTime;

    private InputSystem_Actions playerInputActions;

    private void Awake()
    {
        playerInputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        playerInputActions.Drive.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Drive.Disable();
    }

    void Start()
    {
        carController = GetComponent<CarController>();
        recordingStartTime = Time.time;
    }

    void Update()
    {
        float currentTime = Time.time - recordingStartTime;

        float throttleInput = playerInputActions.Drive.Throttle.ReadValue<float>();
        float brakeInput = playerInputActions.Drive.Brake.ReadValue<float>();
        float steerInput = playerInputActions.Drive.Steer.ReadValue<float>();

        if (throttleInput > 0.5f)
        {
            Record(new AccelerateCommand { Timestamp = currentTime });
        }

        if (brakeInput > 0.5f)
        {
            Record(new BrakeCommand { Timestamp = currentTime });
        }

        if (Mathf.Abs(steerInput) > 0.5f)
        {
            Record(new TurnCommand(steerInput) { Timestamp = currentTime });
        }
    }

    void Record(ICommand command)
    {
        command.Execute(carController);
        recorded.Add(command);
    }

    public List<ICommand> GetRecordedCommands() => recorded;
}
