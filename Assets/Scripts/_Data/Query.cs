using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Query
{
    public float Height;
    public float Moisture;
    public float Heat;
    public WorldMap worldMap;

    /// <summary>
    /// Evaluate whether a query satisfies a biome within a particular worldmap.
    /// </summary>
    public bool Satisfies(Biome biome)
    {
        var normalizedMinHeight = worldMap.HeightMap.MinValue + biome.MinHeight * (worldMap.HeightMap.MaxValue - worldMap.HeightMap.MinValue);
        var normalizedMinMoisture = worldMap.MoistureMap.MinValue + biome.MinMoisture * (worldMap.MoistureMap.MaxValue - worldMap.MoistureMap.MinValue);
        var normalizedMinHeat = worldMap.HeatMap.MinValue + biome.MinHeat * (worldMap.HeatMap.MaxValue - worldMap.HeatMap.MinValue);
        return Height >= normalizedMinHeight && Moisture >= normalizedMinMoisture && Heat >= normalizedMinHeat;
    }

    /// <summary>
    /// Evaluate whether a query satisfies a biome within a particular worldmap.
    /// </summary>
    public float Difference(Biome biome)
    {
        var normalizedMinHeight = worldMap.HeightMap.MinValue + biome.MinHeight * (worldMap.HeightMap.MaxValue - worldMap.HeightMap.MinValue);
        var normalizedMinMoisture = worldMap.MoistureMap.MinValue + biome.MinMoisture * (worldMap.MoistureMap.MaxValue - worldMap.MoistureMap.MinValue);
        var normalizedMinHeat = worldMap.HeatMap.MinValue + biome.MinHeat * (worldMap.HeatMap.MaxValue - worldMap.HeatMap.MinValue);
        return (Height - normalizedMinHeight) + (Moisture - normalizedMinMoisture) + (Heat - normalizedMinHeat);
    }
}
