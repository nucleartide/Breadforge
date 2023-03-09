using UnityEngine;

/// <summary>
/// Note: this should be a class so we can perform null checks.
/// </summary>
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
