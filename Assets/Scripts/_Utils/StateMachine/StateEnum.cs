using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// An enum implemented as a ScriptableObject, where the enum represents a set of StateBehaviours.
/// </summary>
[CreateAssetMenu]
public class StateEnum : ScriptableObject
{
    [SerializeField]
    List<StateEnumValue> enumValues = new List<StateEnumValue>();

    public int GetIndex(StateBehaviour state)
    {
        var stateClassName = state.GetType().Name;
        foreach (var value in enumValues)
        {
            var scriptName = value.State.name;
            if (stateClassName == value.State.name)
                return value.Index;
        }

        throw new System.Exception($"State {stateClassName} does not have a corresponding StateEnumValue. Please configure a new StateEnumValue, add it to list of enumValues, and try again.");
    }
}
