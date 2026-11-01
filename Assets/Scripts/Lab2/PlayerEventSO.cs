using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerEventSO", menuName = "Lab2/PlayerEventSO")]
public class PlayerEventSO : ScriptableObject
{
    public event Action<int> OnPlayerHealthChanged;

    public void NotifyPlayerHealthChanged(int health)
    {
        OnPlayerHealthChanged?.Invoke(health);
    }
}
