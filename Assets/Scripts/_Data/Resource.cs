using System;
using UnityEngine;

public class Resource
{
    public float Quantity;
    public CollectionRate CollectionRate;
    public event EventHandler<PlaceholderArgs> OnDepleted;
    public event EventHandler<PlaceholderArgs> OnCollectCompleted;

    // Count this down to zero.
    public float RemainingTime;

    public void ElapseCollectTime()
    {
        // TODO.
    }
}

public class CollectionRate
{
    public float TimeInterval;
    public float Quantity;
}

public class PlaceholderArgs
{
}

public interface IState
{
    public void Update();
}

public class TempPlayerCollect
{
    [SerializeField]
    [NotNull]
    PlayerCollectableRadius playerCollectableRadius;

    [SerializeField]
    [NotNull]
    InputManager inputManager;

    public class PlayerNotCollectingState : IState
    {
        void IState.Update()
        {
            throw new NotImplementedException();
        }
    }

    public class PlayerCollectingState : IState
    {
        GameObject gameObjectBeingCollected;

        public PlayerCollectingState(GameObject gameObjectBeingCollected)
        {
            this.gameObjectBeingCollected = gameObjectBeingCollected;
        }

        void IState.Update()
        {
            throw new NotImplementedException();
        }
    }

    // A state is needed so that I can communicate to Mecanim that we are in a particular animation state.
    IState currentState;

    // TODO: add this to list of event handlers
    private void InputManager_OnCollectStarted()
    {
        // if we are not near a collectible as reported by playerCollectibleRadius,
        // play a "null" sound.
        // else, if we are near a collectible,
        // change state to collecting
    }

    private void Update()
    {
        // if we are in the collecting state,
        // call ElapseCollectTime() on the connecteed resource
        // todo: use the state pattern, since in the collecting state you need an item to collect
    }

    // TODO: add this to list of event handlers
    private void InputManager_OnCollectPerformed()
    {
        // if we are not in the collecting state, do nothing
        // else, change state to not collecting
    }

    // TODO: add this to list of event handlers
    private void Resource_OnDepleted()
    {
        // ...
        // TODO: play some signifier feedback? maybe floating disappearing text?
    }

    // TODO: add this to list of event handlers
    private void Resource_OnCollectCompleted()
    {
        // ...
        // TODO: play some signifier feedback? maybe floating disappearing text?
    }

    private void Release()
    {
        playerState = PlayerState.NotCollecting;
    }
}
