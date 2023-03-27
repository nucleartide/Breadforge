using UnityEngine;

[CreateAssetMenu]
public class AnimationEnum : ScriptableObject
{
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
}
