using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

[CreateAssetMenu]
public class GridManager : ScriptableObject
{
    #if false
    private static Biome ClosestBiome(BiomeManager biomes, List<Biome> biomePresets, float[,] heightMap, float[,] moistureMap, float[,] heatMap, int x, int y)
    {
        float height = heightMap[y, x];
        float moisture = moistureMap[y, x];
        float heat = heatMap[y, x];

        // Find all the matching biomes.
        var matchingBiomes = biomePresets.FindAll(biome => biome.Match(biomes, height, moisture, heat));
        if (matchingBiomes.Count == 0)
            return null;

        // For each matching biome, find the difference value.
        var matchingBiomesWithDifference = matchingBiomes.ConvertAll(biome =>
        {
            var normalizedMinHeight = biome.MinHeight * (biomes.HeightMax - biomes.HeightMin) + biomes.HeightMin;
            var normalizedMinMoisture = biome.MinMoisture * (biomes.MoistureMax - biomes.MoistureMin) + biomes.MoistureMin;
            var normalizedMinHeat = biome.MinHeat * (biomes.HeatMax - biomes.HeatMin) + biomes.HeatMin;
            return (biome, (height - normalizedMinHeight) + (moisture - normalizedMinMoisture) + (heat - normalizedMinHeat));
        });

        // Find the biome with the minimum difference value. (It seems this version of LINQ has no MinBy.)
        var minValue = 1000f;
        Biome minBiome = null;
        foreach (var (biome, difference) in matchingBiomesWithDifference)
        {
            if (difference < minValue)
            {
                minValue = difference;
                minBiome = biome;
            }
        }

        // Sanity check and return.
        Assert.IsNotNull(minBiome);
        return minBiome;
    }

    [Serializable]
    private struct Wave
    {
        public float Seed;
        public float Frequency;
        public float Amplitude;
    }

    [Serializable]
    private class WorldConfig
    {
        public List<Wave> Height = new List<Wave>
        {
            new Wave {
                Seed = 56f,
                Frequency = .05f,
                Amplitude = 1f,
            },
            new Wave {
                Seed = 199.36f,
                Frequency = .1f,
                Amplitude = .5f,
            },
        };

        public List<Wave> Moisture = new List<Wave>
        {
            new Wave {
                Seed = 621f,
                Frequency = .03f,
                Amplitude = 1f,
            },
        };

        public List<Wave> Heat = new List<Wave>
        {
            new Wave {
                Seed = 318.6f,
                Frequency = .04f,
                Amplitude = 1f,
            },
            new Wave {
                Seed = 329.7f,
                Frequency = .02f,
                Amplitude = .5f,
            },
        };

        public int GridWidth = 100;

        public int GridHeight = 100;

        public WorldInstantiateMode WorldInstantiateMode = WorldInstantiateMode.Height;
    }

    [Serializable]
    public enum WorldInstantiateMode
    {
        None,
        Height,
        Moisture,
        Heat,
    }

    [Serializable]
    private class World
    {
        public NoiseMap HeightMap;
        public NoiseMap MoistureMap;
        public NoiseMap HeatMap;
        public WorldConfig WorldConfig;
        public List<Transform> DebugObjects;
    }

    [SerializeField]
    private WorldConfig worldConfig = new WorldConfig();

    [SerializeField]
    [NotNull]
    private BiomeManager biomeManager;

    [SerializeField]
    [NotNull]
    private Transform cubePrefab;

    [SerializeField]
    [NotNull]
    private MaterialManager materialManager;

    private World world;

    private class NoiseMap
    {
        public float[,] Map;
        public float MinValue;
        public float MaxValue;
    }

    /// <param name="width">Width of the generated map.</param>
    /// <param name="height">Height of the generated map.</param>
    /// <param name="stackOfWaves">The stack of waves to combine into one.</param>
    /// <param name="scale">How zoomed-in the map will be. Pass in a value of 1.0 for no zooming.</param>
    /// <param name="offset">The offset used when sampling from Perlin noise. Pass in a value of Vector2.zero for no offset.</param>
    private static NoiseMap GenerateNoiseMap(int width, int height, List<Wave> stackOfWaves, float scale, Vector2 offset)
    {
        var noiseMap = new float[height, width];
        var minValue = float.MaxValue;
        var maxValue = float.MinValue;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var sampleX = x * scale + offset.x;
                var sampleY = y * scale + offset.y;
                var normalization = 0.0f; // Needed to normalize to [0,1] after summing the stack of noise values.

                foreach (var wave in stackOfWaves)
                {
                    noiseMap[y, x] += wave.Amplitude * Mathf.Clamp01(Mathf.PerlinNoise(sampleX * wave.Frequency + wave.Seed, sampleY * wave.Frequency + wave.Seed));
                    normalization += wave.Amplitude;
                }

                noiseMap[y, x] /= normalization;
                if (noiseMap[y, x] < minValue)
                    minValue = noiseMap[y, x];
                if (noiseMap[y, x] > maxValue)
                    maxValue = noiseMap[y, x];
            }
        }

        return new NoiseMap
        {
            Map = noiseMap,
            MinValue = minValue,
            MaxValue = maxValue,
        };
    }

    private static World GenerateWorldMap(WorldConfig worldConfig)
    {
        var w = worldConfig.GridWidth;
        var h = worldConfig.GridHeight;

        return new World
        {
            HeightMap = GenerateNoiseMap(w, h, worldConfig.Height, 1f, Vector2.zero),
            MoistureMap = GenerateNoiseMap(w, h, worldConfig.Moisture, 1f, Vector2.zero),
            HeatMap = GenerateNoiseMap(w, h, worldConfig.Heat, 1f, Vector2.zero),
            WorldConfig = worldConfig,
        };
    }

    private void InstantiateWorldMap(World world)
    {
        var gridHeight = world.HeatMap.Map.GetLength(0);
        var gridWidth = world.HeatMap.Map.GetLength(1);
        world.DebugObjects = new List<Transform>();

        for (var y = 0; y < gridHeight; y++)
        {
            for (var x = 0; x < gridWidth; x++)
            {
                // Create.
                var cube = Instantiate(cubePrefab);
                world.DebugObjects.Add(cube);

                // Set world instantiate mode.
                if (world.WorldConfig.WorldInstantiateMode == WorldInstantiateMode.None)
                {
                    // List of biomes.
                    var biomes = new List<Biome>
                    {
                        biomeManager.landBiome,
                        biomeManager.waterBiome,

                        biomeManager.stoneBiome,
                        biomeManager.coalBiome,
                        biomeManager.copperOreBiome,
                        biomeManager.ironOreBiome,

                        biomeManager.woodBiome,
                        biomeManager.sugarCaneBiome,
                        biomeManager.wheatBiome,
                    };

                    // First, determine the closest biome.
                    var biome = ClosestBiome(biomeManager, biomes, world.HeightMap.Map, world.MoistureMap.Map, world.HeatMap.Map, x, y);

                    // Then given the biome, set the material of the cube.
                    var map = new Dictionary<Biome, Material>
                    {
                        {biomeManager.waterBiome, materialManager.Water},
                        {biomeManager.coalBiome, materialManager.Coal},
                        {biomeManager.copperOreBiome, materialManager.CopperOre},
                        // {groundBiome, materialManager.},
                        {biomeManager.woodBiome, materialManager.Wood},
                        {biomeManager.ironOreBiome, materialManager.IronOre},
                        {biomeManager.stoneBiome, materialManager.Stone},
                        {biomeManager.sugarCaneBiome, materialManager.SugarCane},
                        {biomeManager.wheatBiome, materialManager.Wheat},
                    };

                    // Set the material if not null.
                    if (biome != null && map.ContainsKey(biome))
                        cube.GetComponentInChildren<MeshRenderer>().material = map[biome];
                    else
                        Debug.Log("not setting material");
                }

                // Set position.
                var position = new Vector3(x - gridWidth / 2, 0f, y - gridHeight / 2);
                cube.transform.position = position;

                // Set scale if not instantiating actual tiles.
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
            }
        }
    }

    private void DestroyWorld(World world)
    {
        if (world.DebugObjects != null)
        {
            foreach (var transform in world.DebugObjects)
            {
                Destroy(transform.gameObject);
            }

            world.DebugObjects = null;
        }
    }

    public void RegenerateTerrain()
    {
        if (world != null)
            DestroyWorld(world);
        world = GenerateWorldMap(worldConfig);
        InstantiateWorldMap(world);
        Debug.Log("Maps have been re-generated.");

            Debug.Log($"Height Min value: {world.HeightMap.MinValue}");
            Debug.Log($"Height Max value: {world.HeightMap.MaxValue}");

            Debug.Log($"Heat Min value: {world.HeatMap.MinValue}");
            Debug.Log($"Heat Max value: {world.HeatMap.MaxValue}");

            Debug.Log($"Moisture Min value: {world.MoistureMap.MinValue}");
            Debug.Log($"Moisture Max value: {world.MoistureMap.MaxValue}");
    }
#endif
}
