using UnityEngine;

public abstract class StateMachineBehaviour : MonoBehaviour
{
    [SerializeField]
    protected StateBehaviour initialState;

    public StateBehaviour CurrentState
    {
        get;
        private set;
    }

    private void Awake() => CurrentState = initialState;

    public void TransitionTo(StateBehaviour newState)
    {
        CurrentState.enabled = false;
        newState.enabled = true;
        CurrentState = newState;
    }
}
