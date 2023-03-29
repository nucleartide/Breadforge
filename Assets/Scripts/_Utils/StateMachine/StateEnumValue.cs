using UnityEngine;

/// <summary>
/// An enum value implemented as a ScriptableObject, where the enum represents a set of StateBehaviours.
/// </summary>
[CreateAssetMenu]
public class StateEnumValue : ScriptableObject
{
    [field: Tooltip("Drag a StateBehaviour script file into this slot.")]
    [field: SerializeField]
    public UnityEditor.MonoScript State
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
