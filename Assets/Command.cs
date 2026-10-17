using UnityEngine;

public interface ICommand
{
    void Execute(CarController car);
    float Timestamp { get; set; }
}

public class AccelerateCommand : ICommand
{
    public float Timestamp { get; set; }
    public void Execute(CarController car) => car.Accelerate();
}

public class BrakeCommand : ICommand
{
    public float Timestamp { get; set; }
    public void Execute(CarController car) => car.Brake();
}

public class TurnCommand : ICommand
{
    public float Timestamp { get; set; }
    private float direction;
    public TurnCommand(float dir) { direction = dir; }
    public void Execute(CarController car) => car.Turn(direction);
}
