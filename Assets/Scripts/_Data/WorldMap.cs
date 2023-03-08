using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public class WorldMap
{
    public NoiseMap HeightMap;
    public NoiseMap MoistureMap;
    public NoiseMap HeatMap;
    private WorldConfig worldConfig;

    public WorldMap(WorldConfig worldConfig)
    {
        this.HeightMap = Noise.GenerateMap(worldConfig.GridWidth, worldConfig.GridHeight, worldConfig.HeightMapConfig, 1f, Vector2.zero);
        this.MoistureMap = Noise.GenerateMap(worldConfig.GridWidth, worldConfig.GridHeight, worldConfig.MoistureMapConfig, 1f, Vector2.zero);
        this.HeatMap = Noise.GenerateMap(worldConfig.GridWidth, worldConfig.GridHeight, worldConfig.HeatMap, 1f, Vector2.zero);
        this.worldConfig = worldConfig;
    }

    private float GetHeight(int x, int y)
    {
        return HeightMap.Map[y, x];
    }

    private float GetMoisture(int x, int y)
    {
        return MoistureMap.Map[y, x];
    }

    private float GetHeat(int x, int y)
    {
        return HeatMap.Map[y, x];
    }

    /// <summary>
    /// Find the closest biome given a world configuration and a set of (x, y) coordinates.
    /// </summary>
    public Biome ClosestBiome(int x, int y)
    {
        // Get noise map values.
        float height = GetHeight(x, y);
        float moisture = GetMoisture(x, y);
        float heat = GetHeat(x, y);

        // Construct query.
        var query = new Query
        {
            Height = height,
            Moisture = moisture,
            Heat = heat,
            worldMap = this,
        };

        // Execute query to determine closest biome.
        var matchingBiomes = worldConfig.FindMatchingBiomes(query);
        var matchingBiomesWithDifference = matchingBiomes.ConvertAll(biome => (biome, query.Difference(biome)));
        var closestBiome = ListHelpers.MinBy(matchingBiomesWithDifference, (a, b) => a.Item2 < b.Item2).biome;

        // Sanity check and return.
        Assert.IsNotNull(closestBiome, "There should always be at least one matching biome, please review the logic here.");
        return closestBiome;
    }

    public void InitializeTile(GameObject cube, WorldDisplayMode worldDisplayMode, int x, int y)
    {
        // Set the cube's position.
        var position = new Vector3(x - worldConfig.GridWidth / 2, 0f, y - worldConfig.GridHeight / 2);
        cube.transform.position = position;

        // Set the cube's material if instantiating for-real tiles.
        if (worldDisplayMode == WorldDisplayMode.ActualTiles)
        {
            // First, determine the closest biome.
            var biome = ClosestBiome(x, y);

            // Then given the biome, set the material of the cube.
            var material = worldConfig.GetMaterial(biome);
            if (material != null)
                cube.GetComponentInChildren<MeshRenderer>().material = material; // shared material?
        }

        // Set the cube's scale if instantiating debug tiles.
        if (worldDisplayMode != WorldDisplayMode.ActualTiles)
        {
            float scale = 0f;
            if (worldDisplayMode == WorldDisplayMode.HeightMap)
                scale = GetHeight(x, y);
            else if (worldDisplayMode == WorldDisplayMode.HeatMap)
                scale = GetHeat(x, y);
            else if (worldDisplayMode == WorldDisplayMode.MoistureMap)
                scale = GetMoisture(x, y);
            scale = Mathf.Clamp01(scale);
            cube.transform.localScale = new Vector3(1f, scale * 20 /* Recall that the pivot is at the cube center, so scaling will extend downward as well. */, 1f);
        }
    }
}
