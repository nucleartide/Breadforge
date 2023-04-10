using UnityEngine;
using System;

/// <summary>
/// A simple state machine implementation that performs state transitions by enabling/disabling MonoBehaviours.
/// </summary>
public abstract class StateMachineBehaviour : MonoBehaviour
{
    public class StateMachineChangedArgs : EventArgs
    {
        public StateBehaviour OldState;
        public StateBehaviour NewState;
    }

    [SerializeField]
    [NotNull]
    protected StateBehaviour initialState;

    public StateBehaviour CurrentState
    {
        get;
        private set;
    }

    public event EventHandler<StateMachineChangedArgs> OnChanged;

    /// <summary>
    /// Must occur after OnEnable, since OnEnable is where the OnChanged listeners are attached.
    /// </summary>
    private void Start()
    {
        CurrentState = initialState;
        OnChanged?.Invoke(this, new StateMachineChangedArgs { OldState = null, NewState = initialState });
    }

    public void TransitionTo(StateBehaviour newState)
    {
        CurrentState.enabled = false;
        newState.enabled = true;
        OnChanged?.Invoke(this, new StateMachineChangedArgs { OldState = CurrentState, NewState = newState });
        CurrentState = newState;
    }
}
