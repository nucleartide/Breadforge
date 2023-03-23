using UnityEngine;

[CreateAssetMenu]
public class PauseManager : Manager
{
    [SerializeField]
    [NotNull]
    InputManager gameInput;

    private bool isPaused = false;

    private void GameInput_OnPauseAction(object sender, float timeOfPause)
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
