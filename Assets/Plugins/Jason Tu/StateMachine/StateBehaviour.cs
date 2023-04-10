using System;
using UnityEngine;

/// <summary>
/// StateBehaviour is used by StateMachineBehaviour to identify MonoBehaviours that are also states.
/// </summary>
public abstract class StateBehaviour : MonoBehaviour
{
    public class TransitionToArgs : EventArgs
    {
        public StateEnumValue NewState;
    }

    public event EventHandler<TransitionToArgs> TransitionTo;
}
