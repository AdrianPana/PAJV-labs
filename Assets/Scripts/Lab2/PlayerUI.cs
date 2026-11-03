using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private PlayerEventSO PlayerEventSO;
    private RectTransform _hpBar;
    private RectTransform _xpBar;

    private void Start()
    {
        transform.GetChild(0).GetChild(0).TryGetComponent(out _hpBar);
        transform.GetChild(1).GetChild(0).TryGetComponent(out _xpBar);
    }

    private void OnEnable()
    {
        PlayerEventSO.OnPlayerHealthChanged += UpdateHealthBar;
        PlayerEventSO.OnPlayerXPChanged += UpdateXPBar;
    }

    private void OnDisable()
    {
        PlayerEventSO.OnPlayerHealthChanged -= UpdateHealthBar;
        PlayerEventSO.OnPlayerXPChanged -= UpdateXPBar;
    }

    private void UpdateHealthBar(Tuple<int, int> hp_info)
    {
        float width = 1000 * ((float)hp_info.Item1 / hp_info.Item2);
        _hpBar.sizeDelta = new Vector2(width, _xpBar.sizeDelta.y);
    }

    private void UpdateXPBar(Tuple<int, int> xp_info)
    {
        float width = 1000 * ((float)xp_info.Item1 / xp_info.Item2);
        _xpBar.sizeDelta = new Vector2(width, _xpBar.sizeDelta.y);
    }
}
