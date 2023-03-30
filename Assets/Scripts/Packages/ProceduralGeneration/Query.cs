public class Query
{
    private WorldMap worldMap;
    private float height;
    private float moisture;
    private float heat;

    private float GetNormalizedMinHeight(Biome biome) =>
        worldMap.MinHeight + biome.MinHeight * (worldMap.MaxHeight - worldMap.MinHeight);

    private float GetNormalizedMinMoisture(Biome biome) =>
        worldMap.MinMoisture + biome.MinMoisture * (worldMap.MaxMoisture - worldMap.MinMoisture);

    private float GetNormalizedMinHeat(Biome biome) =>
        worldMap.MinHeat + biome.MinHeat * (worldMap.MaxHeat - worldMap.MinHeat);

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
