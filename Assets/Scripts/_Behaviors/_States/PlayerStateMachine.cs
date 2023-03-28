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
    private PlayerMiningState playerMiningState;

    [SerializeField]
    [NotNull]
    private PlayerChoppingState playerChoppingState;

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

        throw new System.Exception("switch to state depending on Resource's resource configuration");
        playerMiningState.Initialize(nearest);
        TransitionTo(playerMiningState);
    }

    private void GameInput_OnCollectCanceled(object sender, GameInputManager.GameInputArgs args)
    {
        TransitionTo(playerMovingState);
    }
}
