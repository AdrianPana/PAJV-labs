using System.Collections.Generic;
using UnityEngine;

public class LevelUpState : IGameState
{
    private readonly GameObject upgradePanel;
    private GameObject firstUpgradeButton;
    private GameObject secondUpgradeButton;

    public LevelUpState(GameObject panel)
    {
        upgradePanel = panel;
    }

    public void Enter()
    {
        Time.timeScale = 0f;
        
        var pool = new List<int> { 0, 1, 2, 3, 4, 5 };
        int firstUpgrade = pool[Random.Range(0, pool.Count)];
        pool.Remove(firstUpgrade);
        int secondUpgrade = pool[Random.Range(0, pool.Count)];
        
        firstUpgradeButton = upgradePanel.transform.GetChild(firstUpgrade).gameObject;
        firstUpgradeButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-360, 0);
        firstUpgradeButton.SetActive(true);

        secondUpgradeButton = upgradePanel.transform.GetChild(secondUpgrade).gameObject;
        secondUpgradeButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(360, 0);
        secondUpgradeButton.SetActive(true);

        upgradePanel.SetActive(true);
        
    }

    public void Exit()
    {
        firstUpgradeButton.SetActive(false);
        secondUpgradeButton.SetActive(false);
        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}