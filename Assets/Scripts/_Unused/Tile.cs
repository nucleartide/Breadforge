using UnityEngine;

[CreateAssetMenu]
public class Tile : ScriptableObject
{
    [field: SerializeField]
    public string Label
    {
        get;
        set;
    }

    [field: SerializeField]
    public Color Hover
    {
        get;
        set;
    }

    [field: SerializeField]
    public Color Idle
    {
        get;
        set;
    }
}
