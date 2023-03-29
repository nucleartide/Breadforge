using UnityEngine;
using System.Collections.Generic;

public class PlayerStateMachine : StateMachineBehaviour
{
    [SerializeField]
    [NotNull]
    private GameInputManager gameInput;

    [SerializeField]
    [NotNull]
    private ImmediateCollectableManager immediateCollectable;

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

    [System.Serializable]
    public class ResourceToPlayerState
    {
        [NotNull]
        public ResourceConfiguration Resource;

        [NotNull]
        public PlayerCollectingState PlayerState;
    }

    [SerializeField]
    private List<ResourceToPlayerState> resourcesToPlayerStates = new List<ResourceToPlayerState>();

    private PlayerCollectingState GetPlayerState(ResourceConfiguration resource)
    {
        foreach (var mapping in resourcesToPlayerStates)
            if (mapping.Resource == resource)
                return mapping.PlayerState;

        throw new System.Exception($"Resource {resource.name} does not have a corresponding PlayerCollectingState. Please double-check the resourcesToPlayerStates mapping, then try again.");
    }

    private void GameInput_OnCollectStarted(object sender, GameInputManager.GameInputArgs args)
    {
        var resource = immediateCollectable.ImmediateCollectable;
        if (resource == null)
            throw new System.Exception("TODO(jason): Add in a 'null' sound here.");

        // Given a Resource, fetch the corresponding player state.
        var collectingState = GetPlayerState(resource.Configuration);

        // Perform state transition.
        collectingState.Initialize(resource);
        TransitionTo(collectingState);
    }

    private void GameInput_OnCollectCanceled(object sender, GameInputManager.GameInputArgs args)
    {
        TransitionTo(playerMovingState);
    }
}
