using UnityEngine;

[CreateAssetMenu]
public class Biome : ScriptableObject
{
    [Tooltip("Ranges from 0 to 1.")]
    [field: SerializeField]
    public float MinHeight
    {
        get;
        set;
    }

    [Tooltip("Ranges from 0 to 1.")]
    [field: SerializeField]
    public float MinMoisture
    {
        get;
        set;
    }

    [Tooltip("Ranges from 0 to 1.")]
    [field: SerializeField]
    public float MinHeat
    {
        get;
        set;
    }

    public bool MatchCondition(float height, float moisture, float heat)
    {
        return height > MinHeight && moisture > MinMoisture && heat > MinHeat;
    }
}
