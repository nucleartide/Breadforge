using System;
using UnityEngine;

/// <summary>
/// StateBehaviour is used by StateMachineBehaviour to identify MonoBehaviours that are also states.
/// </summary>
public abstract class StateBehaviour : MonoBehaviour
{
    public class TransitionToArgs : EventArgs
    {
        public StateBehaviour NewState;
    }

    public event EventHandler<TransitionToArgs> OnTransitionTo;

    protected void TransitionTo(StateBehaviour newState)
    {
        OnTransitionTo?.Invoke(this, new TransitionToArgs { NewState = newState });
    }
}
