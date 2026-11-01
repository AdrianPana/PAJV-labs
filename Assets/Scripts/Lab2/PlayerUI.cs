using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private PlayerEventSO PlayerEventSO;
    private TextMeshProUGUI _hpText;

    private void Start()
    {
        transform.GetChild(0).TryGetComponent(out _hpText);
    }

    private void OnEnable()
    {
        PlayerEventSO.OnPlayerHealthChanged += UpdateHealthText;
    }

    private void OnDisable()
    {
        PlayerEventSO.OnPlayerHealthChanged -= UpdateHealthText;
    }

    private void UpdateHealthText(int health)
    {
        _hpText.text = $"HP: {health}";
    }
}
