using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public static GameStateMachine Instance { get; private set; }

    private IGameState currentState;

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

    public void ChangeState(IGameState next)
    {
        currentState?.Exit();
        currentState = next;
        currentState?.Enter();
    }
}
