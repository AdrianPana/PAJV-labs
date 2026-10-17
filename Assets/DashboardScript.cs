using TMPro;
using UnityEngine;

public class DashboardScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private CarController carController;

    private void Start()
    {
        if (carController != null)
        {
            carController.OnSpeedChanged += UpdateSpeedText;
        }
    }

    private void UpdateSpeedText(float speed)
    {
        speedText.SetText($"Smecherometru: {speed:F1} km/h");
    }
}
