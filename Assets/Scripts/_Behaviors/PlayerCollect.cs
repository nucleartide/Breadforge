using UnityEngine;

public abstract class StateMachineBehaviour : MonoBehaviour
{
    private void DisableAllStates()
    {
        var states = GetComponents<State>();
        foreach (var state in states)
            state.enabled = false;
    }

    public void TransitionTo(State newState)
    {
        DisableAllStates();
        newState.enabled = true;
    }

    public State CurrentState
    {
        get
        {
            var states = GetComponents<State>();
            foreach (var state in states)
                if (state.enabled)
                    return state;

            return null;
        }
    }
}

[RequireComponent(typeof(PlayerCollectableRadius))]
public class PlayerCollect : StateMachineBehaviour
{
    [SerializeField]
    [NotNull]
    PlayerCollectableRadius playerCollectableRadius;

    [SerializeField]
    [NotNull]
    State initialState;

    [SerializeField]
    [NotNull]
    PlayerCollectingState playerCollectingState;

    [SerializeField]
    [NotNull]
    PlayerNotCollectingState playerNotCollectingState;

    [SerializeField]
    [NotNull]
    GameInputManager gameInput;

    [SerializeField]
    [NotNull]
    PlayerConfiguration playerConfiguration;

    private void OnEnable()
    {
        TransitionTo(initialState);
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
        Debug.Log("hello");

        var nearest = playerCollectableRadius.CanCollectResource;
        if (nearest == null)
            throw new System.Exception("TODO: Jason add in a 'null' sound here.");

        playerCollectingState.Initialize(nearest);
        TransitionTo(playerCollectingState);
    }

    private void GameInput_OnCollectCanceled(object sender, GameInputManager.GameInputArgs args)
    {
        Debug.Log("canceled");

        if (CurrentState == playerCollectingState)
            TransitionTo(playerNotCollectingState);
    }

    private void Update()
    {
        if (CurrentState != playerNotCollectingState)
        {
            var isCollecting = gameInput.GetCollect();
            var isMoving = gameInput.GetMovement() != Vector3.zero;

            if (!isCollecting && isMoving)
                TransitionTo(playerNotCollectingState);

            if (playerCollectableRadius.CanCollectResource == null)
                TransitionTo(playerNotCollectingState);
        }
    }

    public bool IsCollecting
    {
        get
        {
            return CurrentState == playerCollectingState;
        }
    }

#if false
    // TODO: add this to list of event handlers
    private void Resource_OnCollectCompleted()
    {
        // ...
        // TODO: play some signifier feedback? maybe floating disappearing text?
    }

    // TODO: add this to list of event handlers
    private void Resource_OnDepleted()
    {
        // ...
        // TODO: play some signifier feedback? maybe floating disappearing text?
    }
#endif
}
