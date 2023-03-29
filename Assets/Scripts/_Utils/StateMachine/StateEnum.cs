using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// An enum implemented as data, where the enum represents a set of StateBehaviours.
/// </summary>
[System.Serializable]
public class StateEnum
{
    [SerializeField]
    private List<StateEnumValue> enumValues;

    public int GetIndex(StateBehaviour state)
    {
        foreach (var value in enumValues)
            if (value.State == state)
                return value.Index;

        throw new System.Exception($"State {state.name} does not have a corresponding StateEnumValue. Please configure a new StateEnumValue, add it to list of EnumValues, and try again.");
    }
}
