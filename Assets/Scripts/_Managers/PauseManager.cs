using UnityEngine;

[CreateAssetMenu]
public class PauseManager : Manager
{
    [SerializeField]
    [NotNull]
    GameInputManager gameInput;

    private bool isPaused = false;

    private void GameInput_OnPauseAction(object sender, GameInputManager.GameInputArgs args)
    {
        TogglePause();
        Debug.Log(isPaused ? "Game is paused." : "Game is active.");
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
    }

    public override void OnManualEnable()
    {
        gameInput.OnPause += GameInput_OnPauseAction;
    }

    public override void OnManualDisable()
    {
        gameInput.OnPause -= GameInput_OnPauseAction;
    }
}
