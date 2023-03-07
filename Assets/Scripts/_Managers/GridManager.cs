using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private WorldConfig worldConfig;

    [SerializeField]
    private WorldInstantiateMode worldInstantiateMode;

    private WorldMap worldMap;

    private List<GameObject> tiles;

    private void Start()
    {
        worldMap = new WorldMap(worldConfig);
        throw new System.Exception("Continue refactoring ClosestBiome().");
    }

    private static Biome ClosestBiome(WorldConfig worldConfig, WorldMap worldMap, int x, int y)
    {
        float height = worldMap.GetHeight(x, y);
        float moisture = worldMap.GetMoisture(x, y);
        float heat = worldMap.GetHeat(x, y);

        var query = new Query
        {
            Height = height,
            Moisture = moisture,
            Heat = heat,
            worldMap = worldMap
        };

        var matchingBiomes = worldConfig.FindMatchingBiomes(query);
        var matchingBiomesWithDifference = matchingBiomes.ConvertAll(biome => (biome, query.Difference(biome)));

        // Find the biome with the minimum difference value. (It seems this version of LINQ has no MinBy.)
        throw new System.Exception("refactor plz");
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

#if false
    // Third.
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

    // Second.
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

    // First.


#endif
}
