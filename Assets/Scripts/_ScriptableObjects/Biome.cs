using UnityEngine;

[System.Serializable]
public struct Biome
{
    [Tooltip("Ranges from 0 to 1.")]
    [field: SerializeField]
    public float MinHeight
    {
        get;
        private set;
    }

    [Tooltip("Ranges from 0 to 1.")]
    [field: SerializeField]
    public float MinMoisture
    {
        get;
        private set;
    }

    [Tooltip("Ranges from 0 to 1.")]
    [field: SerializeField]
    public float MinHeat
    {
        get;
        private set;
    }

    /// <summary>
    /// Evaluate whether a set of (height, moisture, heat) values matches this biome.
    /// </summary>
    public bool Match(Biomes biomeManager, float height, float moisture, float heat)
    {
        // given a biome, and a set of (height, moisture, heat) values,
        // check whether there is a match.
        // Match(biome, height, moisture, heat)
        throw new System.Exception("jason: move this into Biomes/BiomeManager");

        var normalizedMinHeight = MinHeight * (biomeManager.HeightMax - biomeManager.HeightMin) + biomeManager.HeightMin;
        var normalizedMinMoisture = MinMoisture * (biomeManager.MoistureMax - biomeManager.MoistureMin) + biomeManager.MoistureMin;
        var normalizedMinHeat = MinHeat * (biomeManager.HeatMax - biomeManager.HeatMin) + biomeManager.HeatMin;

        return height >= normalizedMinHeight && moisture >= normalizedMinMoisture && heat >= normalizedMinHeat;
    }
}
