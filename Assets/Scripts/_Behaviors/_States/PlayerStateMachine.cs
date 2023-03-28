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
        var resource = playerCollectableRadius.CanCollectResource;
        if (resource == null)
            throw new System.Exception("TODO: Jason add in a 'null' sound here.");

        // Grab some data.
        var config = resource.ResourceConfiguration;
        var stateName = config.PlayerStateEnum.State.name;

        // Given a Resource, fetch the corresponding player state.
        PlayerCollectingState collectingState;
        if (stateName == playerMiningState.GetType().Name)
            collectingState = playerMiningState;
        else if (stateName == playerChoppingState.GetType().Name)
            collectingState = playerChoppingState;
        else
            throw new System.Exception($"State name {stateName} does not have a corresponding PlayerCollectingState. Please inspect the source code to figure out what's going on.");

        throw new System.Exception("update player TOol script to show player axe when chopping");

        // Perform state transition.
        collectingState.Initialize(resource);
        TransitionTo(collectingState);
    }

    private void GameInput_OnCollectCanceled(object sender, GameInputManager.GameInputArgs args)
    {
        TransitionTo(playerMovingState);
    }
}
