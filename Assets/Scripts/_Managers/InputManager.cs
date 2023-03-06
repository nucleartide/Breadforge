using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    public event EventHandler<float> OnPauseAction;

    private void Awake()
    {
        // Enable the "Player" action map and attach listeners.
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        // Detach listeners and dispose of held-onto state.
        if (playerInputActions != null)
        {
            playerInputActions.Player.Pause.performed -= Pause_performed;
            playerInputActions.Dispose();
        }
    }

    private void Pause_performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, Time.time);
    }

    public Vector3 GetMovement()
    {
        var move = playerInputActions.Player.Move.ReadValue<Vector2>();
        return Vector3.ClampMagnitude(new Vector3(move.x, 0f, move.y), 1f);
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
