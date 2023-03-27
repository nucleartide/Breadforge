using UnityEngine;
using UnityEngine.Assertions;
using System;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class GameInputManager : Manager
{
    private PlayerInputActions playerInputActions;

    public class GameInputArgs : EventArgs
    {
        public float CurrentTime;
    }

    public event EventHandler<GameInputArgs> OnPaused;

    public event EventHandler<GameInputArgs> OnCollectStarted;

    public event EventHandler<GameInputArgs> OnCollectPerformed;

    public event EventHandler<GameInputArgs> OnCollectCanceled;

    public override void OnManualEnable()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Pause.performed += Pause_performed;
        playerInputActions.Player.Collect.started += Collect_started;
        playerInputActions.Player.Collect.performed += Collect_performed;
        playerInputActions.Player.Collect.canceled += Collect_canceled;
    }

    public override void OnManualDisable()
    {
        playerInputActions.Player.Pause.performed -= Pause_performed;
        playerInputActions.Player.Collect.started -= Collect_started;
        playerInputActions.Player.Collect.performed -= Collect_performed;
        playerInputActions.Player.Collect.canceled -= Collect_canceled;
        playerInputActions.Dispose();
        playerInputActions = null;
    }

    private void Pause_performed(InputAction.CallbackContext context)
    {
        OnPaused?.Invoke(this, new GameInputArgs { CurrentTime = Time.time });
    }

    private void Collect_started(InputAction.CallbackContext context)
    {
        OnCollectStarted?.Invoke(this, new GameInputArgs { CurrentTime = Time.time });
    }

    private void Collect_performed(InputAction.CallbackContext context)
    {
        OnCollectPerformed?.Invoke(this, new GameInputArgs { CurrentTime = Time.time });
    }

    private void Collect_canceled(InputAction.CallbackContext context)
    {
        OnCollectCanceled?.Invoke(this, new GameInputArgs { CurrentTime = Time.time });
    }

    public Vector3 GetMovement()
    {
        Assert.IsNotNull(playerInputActions);
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

    public bool GetCollect()
    {
        return playerInputActions.Player.Collect.IsPressed();
    }
}
