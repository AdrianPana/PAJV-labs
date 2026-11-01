using Lab2;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Input_2D_Actions playerActions;

    private void Awake()
    {
        playerActions = new Input_2D_Actions();
    }

    private void OnEnable()
    {
        playerActions.Player.Enable();
    }

    private void OnDisable()
    {
        playerActions.Player.Disable();
    }

    private void Update()
    {
        Vector2 input = playerActions.Player.Move.ReadValue<Vector2>();

        transform.position += new Vector3(input.x, input.y, 0) * Time.deltaTime * moveSpeed;
    }
}
