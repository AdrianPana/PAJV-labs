using System.Collections.Generic;
using UnityEngine;

public class GhostCarPlayer : MonoBehaviour
{
    private CarController carController;
    public List<ICommand> replayCommands = new List<ICommand>();

    private int currentIndex = 0;
    private float startTime;

    private void Start()
    {
        carController = GetComponent<CarController>();
        startTime = Time.time;
    }

    private void FixedUpdate()
    {
        if (currentIndex >= replayCommands.Count)
            return;

        float elapsedTime = Time.time - startTime;
        ICommand command = replayCommands[currentIndex];

        if (elapsedTime >= command.Timestamp)
        {
            command.Execute(carController);
            currentIndex++;
        }

    }
}
