using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldMap
{
    public NoiseMap HeightMap;
    public NoiseMap MoistureMap;
    public NoiseMap HeatMap;

    public WorldMap(WorldConfig worldConfig)
    {
        this.HeightMap = Noise.GenerateMap(worldConfig.GridWidth, worldConfig.GridHeight, worldConfig.HeightMapConfig, 1f, Vector2.zero);
        this.MoistureMap = Noise.GenerateMap(worldConfig.GridWidth, worldConfig.GridHeight, worldConfig.MoistureMapConfig, 1f, Vector2.zero);
        this.HeatMap = Noise.GenerateMap(worldConfig.GridWidth, worldConfig.GridHeight, worldConfig.HeatMap, 1f, Vector2.zero);
    }

    public float GetHeight(int x, int y)
    {
        return HeightMap.Map[y, x];
    }

    public float GetMoisture(int x, int y)
    {
        return MoistureMap.Map[y, x];
    }

    public float GetHeat(int x, int y)
    {
        return HeatMap.Map[y, x];
    }
}
