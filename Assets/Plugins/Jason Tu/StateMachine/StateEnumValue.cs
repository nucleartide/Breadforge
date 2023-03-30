using UnityEngine;

/// <summary>
/// An enum value implemented as data, where the enum represents a set of StateBehaviours.
/// </summary>
[System.Serializable]
public class StateEnumValue
{
    [field: Tooltip("Drag a StateBehaviour script file into this slot.")]
    [field: SerializeField]
    public StateBehaviour State
    {
        get;
        private set;
    }

    [field: SerializeField]
    public int Index
    {
        get;
        private set;
    }
}
