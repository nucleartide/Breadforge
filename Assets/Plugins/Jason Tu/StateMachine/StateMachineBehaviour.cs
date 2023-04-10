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
    }

    private void OnDisable()
    {
        if (CurrentState != null)
            CurrentState.OnTransitionTo -= StateBehaviour_OnTransitionTo;
    }

    protected void TransitionTo(StateBehaviour newState)
    {
        // Disable current state, if there is any.
        if (CurrentState != null)
        {
            CurrentState.enabled = false;
            CurrentState.OnTransitionTo -= StateBehaviour_OnTransitionTo;
        }

        // Enable new state.
        newState.enabled = true;
        newState.OnTransitionTo += StateBehaviour_OnTransitionTo;

        // Emit change event.
        OnChanged?.Invoke(this, new StateMachineChangedArgs { OldState = CurrentState, NewState = newState });

        // Update current state reference.
        CurrentState = newState;

        Debug.Log("Changed state:" + newState);
    }

    private void StateBehaviour_OnTransitionTo(object sender, StateBehaviour.TransitionToArgs args)
    {
        TransitionTo(args.NewState);
    }
}
