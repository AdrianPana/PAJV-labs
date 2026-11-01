using TMPro;
using UnityEngine;

public class DashboardScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private CarController carController;

    private void Start()
    {
        if (carController != null)
        {
            carController.OnSpeedChanged += UpdateSpeedText;
            carController.TimerChanged += UpdateTimeText;
        }
    }

    private void UpdateSpeedText(float speed)
    {
        speedText.SetText($"Smecherometru: {speed:F1} km/h");
    }

    private void UpdateTimeText(float time)
    {
        timeText.SetText($"Rolexu' de la mana: {time:F2} secunde");
    }
}
