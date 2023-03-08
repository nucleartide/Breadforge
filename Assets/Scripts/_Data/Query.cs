public class Query
{
    private WorldMap worldMap;
    private float height;
    private float moisture;
    private float heat;

    private float GetNormalizedMinHeight(Biome biome) =>
        worldMap.HeightMap.MinValue + biome.MinHeight * (worldMap.HeightMap.MaxValue - worldMap.HeightMap.MinValue);

    private float GetNormalizedMinMoisture(Biome biome) =>
        worldMap.MoistureMap.MinValue + biome.MinMoisture * (worldMap.MoistureMap.MaxValue - worldMap.MoistureMap.MinValue);

    private float GetNormalizedMinHeat(Biome biome) =>
        worldMap.HeatMap.MinValue + biome.MinHeat * (worldMap.HeatMap.MaxValue - worldMap.HeatMap.MinValue);

    public Query(WorldMap worldMap, float height, float moisture, float heat)
    {
        this.worldMap = worldMap;
        this.height = height;
        this.moisture = moisture;
        this.heat = heat;
    }

    /// <summary>
    /// Evaluate whether a query satisfies a biome.
    /// </summary>
    public bool Satisfies(Biome biome)
    {
        var normalizedMinHeight = GetNormalizedMinHeight(biome);
        var normalizedMinMoisture = GetNormalizedMinMoisture(biome);
        var normalizedMinHeat = GetNormalizedMinHeat(biome);
        return height >= normalizedMinHeight && moisture >= normalizedMinMoisture && heat >= normalizedMinHeat;
    }

    /// <summary>
    /// Compute the difference value for a given biome.
    /// </summary>
    public float Difference(Biome biome)
    {
#if UNITY_EDITOR
        if (!Satisfies(biome))
            throw new System.Exception("Biome must satisfy query in order to compute difference.");
#endif

        var normalizedMinHeight = GetNormalizedMinHeight(biome);
        var normalizedMinMoisture = GetNormalizedMinMoisture(biome);
        var normalizedMinHeat = GetNormalizedMinHeat(biome);
        return (height - normalizedMinHeight) + (moisture - normalizedMinMoisture) + (heat - normalizedMinHeat);
    }
}
