using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class AnimationEnum : ScriptableObject
{
    [field: Tooltip("Drag a StateBehaviour script file into this slot.")]
    [field: SerializeField]
    public Object State
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

    public static int GetIndex(StateBehaviour state, List<AnimationEnum> animationEnums)
    {
        var stateClassName = state.GetType().Name;
        foreach (var animEnum in animationEnums)
        {
            var scriptName = animEnum.State.name;
            if (stateClassName == animEnum.State.name)
                return animEnum.Index;
        }

        throw new System.Exception($"Current state {stateClassName} does not have an animation index in animationEnums. Please configure a new AnimationEnum, add it to list of animationEnums, and try again.");
    }
}
