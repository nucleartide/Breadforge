using UnityEngine;

[System.Serializable]
public class Biome
{
    [Tooltip("Ranges from 0 to 1.")]
    public float MinHeight;

    [Tooltip("Ranges from 0 to 1.")]
    public float MinMoisture;

    [Tooltip("Ranges from 0 to 1.")]
    public float MinHeat;
}
