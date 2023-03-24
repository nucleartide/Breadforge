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
        Debug.Log("attaching collect handlers");
        TransitionTo(initialState);
        gameInput.OnCollectStarted += GameInput_OnCollectStarted;
    }

    private void OnDisable()
    {
        Debug.Log("detaching collect handlers");
        gameInput.OnCollectStarted -= GameInput_OnCollectStarted;
    }

    private void GameInput_OnCollectStarted(object sender, GameInputManager.GameInputArgs args)
    {
        Debug.Log("hello");

        var nearest = playerCollectableRadius.CanCollectResource;
        if (nearest == null)
            throw new System.Exception("TODO: Jason add in a 'null' sound here.");

        playerCollectingState.Initialize(nearest.transform);
        TransitionTo(playerCollectingState);
    }

    private void Update()
    {
        var isCollecting = gameInput.GetCollect();
        var isMoving = gameInput.GetMovement() != Vector3.zero;
        if (!isCollecting && isMoving && CurrentState != playerNotCollectingState)
            TransitionTo(playerNotCollectingState);
    }

    public bool IsCollecting
    {
        get
        {
            return CurrentState == playerCollectingState;
        }
    }

#if false
    private void InputManager_OnCollectStarted()
    {
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
        // playerState = PlayerState.NotCollecting;
    }

    // TODO: test out the character's movement. it shouldn't interfere with mining.
#endif
}
