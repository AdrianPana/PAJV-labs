using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerEventSO", menuName = "Lab2/PlayerEventSO")]
public class PlayerEventSO : ScriptableObject
{
    public event Action<Tuple<int, int>> OnPlayerHealthChanged;
    public event Action<Tuple<int, int>> OnPlayerXPChanged;

    public void NotifyPlayerHealthChanged(int health, int maxHealth)
    {
        OnPlayerHealthChanged?.Invoke(new Tuple<int, int>(health, maxHealth));
    }

    public void NotifyPlayerXPChanged(int xp, int levelUpThreshold)
    {
        OnPlayerXPChanged?.Invoke(new Tuple<int, int>(xp, levelUpThreshold));
    }
}
