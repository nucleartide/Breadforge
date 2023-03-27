using UnityEngine;

public class PlayerStateMachine : StateMachineBehaviour
{
    [SerializeField]
    [NotNull]
    private GameInputManager gameInput;

    [SerializeField]
    [NotNull]
    private PlayerCollectableRadius playerCollectableRadius;

    [SerializeField]
    [NotNull]
    private PlayerCollectingState playerCollectingState;

    [SerializeField]
    [NotNull]
    private PlayerMovingState playerMovingState;

    private void OnEnable()
    {
        gameInput.OnCollectStarted += GameInput_OnCollectStarted;
        gameInput.OnCollectCanceled += GameInput_OnCollectCanceled;
    }

    private void OnDisable()
    {
        gameInput.OnCollectStarted -= GameInput_OnCollectStarted;
        gameInput.OnCollectCanceled -= GameInput_OnCollectCanceled;
    }

    private void GameInput_OnCollectStarted(object sender, GameInputManager.GameInputArgs args)
    {
        var nearest = playerCollectableRadius.CanCollectResource;
        if (nearest == null)
            throw new System.Exception("TODO: Jason add in a 'null' sound here.");

        playerCollectingState.Initialize(nearest);
        TransitionTo(playerCollectingState);
    }

    private void GameInput_OnCollectCanceled(object sender, GameInputManager.GameInputArgs args)
    {
        TransitionTo(playerMovingState);
    }
}
