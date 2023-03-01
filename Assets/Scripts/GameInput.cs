using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        // Enable the "Player" action map.
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementNormalized()
    {
        return playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
    }

    public Vector2 GetLookAround()
    {
        return playerInputActions.Player.LookAround.ReadValue<Vector2>();
    }

    public bool GetRun()
    {
        return playerInputActions.Player.Run.IsPressed();
    }
}
