using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CarController carController = other.GetComponent<CarController>();
        if (carController != null)
        {
            carController.FinishLap();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CarController carController = other.GetComponent<CarController>();
        if (carController != null)
        {
            carController.LeftFinishLine();
        }
    }
}
