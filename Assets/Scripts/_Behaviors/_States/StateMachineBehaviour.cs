using UnityEngine;
using System;

public abstract class StateMachineBehaviour : MonoBehaviour
{
    [SerializeField]
    protected StateBehaviour initialState;

    public StateBehaviour CurrentState
    {
        get;
        private set;
    }

    private void Awake()
    {
        CurrentState = initialState;
        OnChanged?.Invoke(this, new StateMachineChangedArgs { OldState = null, NewState = initialState });
    }

    public class StateMachineChangedArgs : EventArgs
    {
        public StateBehaviour OldState;
        public StateBehaviour NewState;
    }

    public event EventHandler<StateMachineChangedArgs> OnChanged;

    public void TransitionTo(StateBehaviour newState)
    {
        CurrentState.enabled = false;
        newState.enabled = true;
        OnChanged?.Invoke(this, new StateMachineChangedArgs { OldState = CurrentState, NewState = newState });
        CurrentState = newState;
        throw new System.Exception("leave off for tomorrow: add remaining animations in Notion");
    }
}
