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

    private GameObject InstantiateTile()
    {
        return null;
#if false
                // Create.
                var cube = Instantiate(cubePrefab);

                // Set the cube's material if instantiating for-real tiles.
                if (world.WorldConfig.WorldInstantiateMode == WorldInstantiateMode.None)
                {
                    // First, determine the closest biome.
                    var biome = ClosestBiome(biomeManager, biomes, world.HeightMap.Map, world.MoistureMap.Map, world.HeatMap.Map, x, y);

                    // Then given the biome, set the material of the cube.
                    var material = worldConfig.GetMaterial(biome);
                    if (material != null)
                        cube.GetComponentInChildren<MeshRenderer>().material = material; // shared material?
                }

                // Set the cube's position.
                var position = new Vector3(x - gridWidth / 2, 0f, y - gridHeight / 2);
                cube.transform.position = position;

                // Set the cube's scale if instantiating debug tiles.
                if (world.WorldConfig.WorldInstantiateMode != WorldInstantiateMode.None)
                {
                    float scale = 0f;
                    if (world.WorldConfig.WorldInstantiateMode == WorldInstantiateMode.Height)
                        scale = world.HeightMap.Map[y, x];
                    else if (world.WorldConfig.WorldInstantiateMode == WorldInstantiateMode.Heat)
                        scale = world.HeatMap.Map[y, x];
                    else if (world.WorldConfig.WorldInstantiateMode == WorldInstantiateMode.Moisture)
                        scale = world.MoistureMap.Map[y, x];
                    scale = Mathf.Clamp01(scale);
                    cube.transform.localScale = new Vector3(1f, scale * 20 /* Recall that the pivot is at the cube center, so scaling will extend downward as well. */, 1f);
                }
#endif
    }

    public void Instantiate(GameObject tilePrefab, WorldInstantiateMode worldInstantiateMode)
    {
        var gridHeight = worldConfig.GridHeight;
        var gridWidth = worldConfig.GridWidth;

        for (var y = 0; y < gridHeight; y++)
        {
            for (var x = 0; x < gridWidth; x++)
            {
                // TODO: finish this up
            }
        }

        // TODO: Return list of instantiated objects so that they may be destroyed when game is in-play.
    }
}
