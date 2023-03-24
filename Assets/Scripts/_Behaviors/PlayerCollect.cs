using UnityEngine;

[RequireComponent(typeof(PlayerCollectableRadius))]
public class PlayerCollect : MonoBehaviour
{
    /// <summary>
    /// The current state of the player's Collect action.
    ///
    /// I use explicit states to visualize these states as animation states in Mecanim.
    /// </summary>
    IState currentState = new PlayerNotCollectingState();

    [SerializeField]
    [NotNull]
    PlayerCollectableRadius playerCollectableRadius;

    [SerializeField]
    [NotNull]
    GameInputManager gameInput;

    [SerializeField]
    [NotNull]
    PlayerConfiguration playerConfiguration;

    private void OnEnable()
    {
        Debug.Log("attaching collect handlers");
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

        currentState = new PlayerCollectingState(nearest.transform, gameObject.transform, playerConfiguration);

        // TODO: fix rotation speed when facing desired rotation
        // TODO: can you move while mining? the answer is no.
    }

    private void Update()
    {
        currentState.Update();
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
