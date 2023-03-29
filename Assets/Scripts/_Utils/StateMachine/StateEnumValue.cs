using UnityEngine;
using System.Collections.Generic;

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
