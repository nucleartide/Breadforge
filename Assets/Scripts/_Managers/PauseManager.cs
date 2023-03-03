using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    InputManager gameInput;

    private bool isPaused = false;

    private void Start()
    {
        gameInput.OnPauseAction += GameInput_OnPauseAction;
    }

    private void OnDestroy()
    {
        gameInput.OnPauseAction -= GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, float timeOfPause)
    {
        TogglePause();
        Debug.Log(isPaused ? "Game is paused." : "Game is active.");
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
    }
}
