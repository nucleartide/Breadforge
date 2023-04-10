using UnityEngine;
using System;

/// <summary>
/// A simple state machine implementation that performs state transitions by enabling/disabling MonoBehaviours.
/// </summary>
public abstract class StateMachineBehaviour : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    protected StateBehaviour initialState;

    public StateBehaviour CurrentState
    {
        get;
        private set;
    }

    public class StateMachineChangedArgs : EventArgs
    {
        public StateBehaviour OldState;
        public StateBehaviour NewState;
    }

    public event EventHandler<StateMachineChangedArgs> OnChanged;

    /// <summary>
    /// Must occur after OnEnable, since OnEnable is where the OnChanged listeners are attached.
    /// </summary>
    private void Start()
    {
        TransitionTo(initialState);
        CurrentState.TransitionTo += StateBehaviour_OnTransitionTo;
    }

    private void OnDisable()
    {
        CurrentState.TransitionTo -= StateBehaviour_OnTransitionTo;
    }

    protected void TransitionTo(StateBehaviour newState)
    {
        // Disable current state, if there is any.
        if (CurrentState != null)
            CurrentState.enabled = false;

        // Enable new state.
        newState.enabled = true;

        // Emit change event.
        OnChanged?.Invoke(this, new StateMachineChangedArgs { OldState = CurrentState, NewState = newState });

        // Update current state reference.
        CurrentState = newState;
    }

    private void StateBehaviour_OnTransitionTo(object sender, StateBehaviour.TransitionToArgs args)
    {
        CurrentState.TransitionTo -= StateBehaviour_OnTransitionTo;

        var newState = args.NewState.State;
        newState.TransitionTo += StateBehaviour_OnTransitionTo;

        TransitionTo(newState);
    }
}
