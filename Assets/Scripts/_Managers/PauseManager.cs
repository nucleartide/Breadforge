using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    [NotNull(IgnorePrefab = true)]
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

    public void OnEnable()
    {
        gameInput.OnPaused += GameInput_OnPauseAction;
    }

    public void OnDisable()
    {
        gameInput.OnPaused -= GameInput_OnPauseAction;
    }
}
