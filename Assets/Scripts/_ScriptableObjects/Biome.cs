using UnityEngine;

[System.Serializable]
public class Biome
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

    public bool MatchCondition(Biomes biomeManager, float height, float moisture, float heat)
    {
        var normalizedMinHeight = MinHeight * (biomeManager.HeightMax - biomeManager.HeightMin) + biomeManager.HeightMin;
        var normalizedMinMoisture = MinMoisture * (biomeManager.MoistureMax - biomeManager.MoistureMin) + biomeManager.MoistureMin;
        var normalizedMinHeat = MinHeat * (biomeManager.HeatMax - biomeManager.HeatMin) + biomeManager.HeatMin;

        return height >= normalizedMinHeight && moisture >= normalizedMinMoisture && heat >= normalizedMinHeat;
    }
}
