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
        Debug.Log($"Starting replay with {replayCommands.Count} commands.");
    }

    private void FixedUpdate()
    {
        Debug.Log($"Current index: {currentIndex}, Total commands: {replayCommands.Count}");

        if (currentIndex >= replayCommands.Count)
            return;

        float elapsedTime = Time.time - startTime;
        while (currentIndex < replayCommands.Count &&
           elapsedTime >= replayCommands[currentIndex].Timestamp)
        {
            replayCommands[currentIndex].Execute(carController);
            currentIndex++;
        }
    }
}
