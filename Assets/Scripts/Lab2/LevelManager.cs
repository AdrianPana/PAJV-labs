using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private PlayerEventSO playerEventSO;
    [SerializeField] private GameObject levelUpPanel;
    public int currentLevel = 1;
    public int levelUpThreshold = 5;

    public int currentXP = 0;
    public float projectileFireRate { get; private set; } = 2.0f;
    public float piercerFireRate { get; private set; } = 2.0f;
    public float currentEnemySpawnInterval { get; private set; } = 3.0f;
    public int enemyHealthBonus { get; private set; } = 0;
    public int enemyTypes { get; private set; } = 1;
    public int maxHp { get; private set; } = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= levelUpThreshold)
        {
            LevelUp();
        }
        playerEventSO.NotifyPlayerXPChanged(currentXP, levelUpThreshold);
    }

    public void LevelUp()
    {
        currentLevel++;
        if (currentLevel % 7 == 0)
        {
            enemyHealthBonus++;
        }
        if (currentLevel == 3)
        {
            enemyTypes = 2;
        }
        else if (currentLevel == 5)
        {
            enemyTypes = 3;
        }
        currentXP -= levelUpThreshold;
        levelUpThreshold = (int)((float) levelUpThreshold * 1.2f);
        currentEnemySpawnInterval *= 0.8f;
        GameStateMachine.Instance.ChangeState(new LevelUpState(levelUpPanel));
    }
}
