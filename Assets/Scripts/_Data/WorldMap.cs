using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public class WorldMap
{
    private WorldConfiguration worldConfig;
    private NoiseMap heightMap;
    private NoiseMap moistureMap;
    private NoiseMap heatMap;

    private float GetHeight(int x, int y) => heightMap.Map[y, x];
    private float GetMoisture(int x, int y) => moistureMap.Map[y, x];
    private float GetHeat(int x, int y) => heatMap.Map[y, x];

    public float MinHeight
    {
        get => heightMap.MinValue;
    }

    public float MaxHeight
    {
        get => heightMap.MaxValue;
    }

    public float MinMoisture
    {
        get => moistureMap.MinValue;
    }

    public float MaxMoisture
    {
        get => moistureMap.MaxValue;
    }

    public float MinHeat
    {
        get => heatMap.MinValue;
    }

    public float MaxHeat
    {
        get => heatMap.MaxValue;
    }

    public WorldMap(WorldConfiguration worldConfig)
    {
        this.worldConfig = worldConfig;
        this.heightMap = Noise.GenerateMap(worldConfig.GridWidth, worldConfig.GridHeight, worldConfig.HeightMapConfig, 1f, Vector2.zero);
        this.moistureMap = Noise.GenerateMap(worldConfig.GridWidth, worldConfig.GridHeight, worldConfig.MoistureMapConfig, 1f, Vector2.zero);
        this.heatMap = Noise.GenerateMap(worldConfig.GridWidth, worldConfig.GridHeight, worldConfig.HeatMapConfig, 1f, Vector2.zero);
    }

    /// <summary>
    /// Find the closest biome given a set of (x, y) coordinates.
    /// </summary>
    public Biome ClosestBiome(int x, int y)
    {
        // Get noise map values.
        float height = GetHeight(x, y);
        float moisture = GetMoisture(x, y);
        float heat = GetHeat(x, y);

        // Construct query.
        var query = new Query(this, height, moisture, heat);

        // Execute query to determine closest biome.
        var matchingBiomes = worldConfig.FindMatchingBiomes(query);
        var matchingBiomesWithDifference = matchingBiomes.ConvertAll(biome =>
        {
            var difference = query.Difference(biome);
            return (biome, difference);
        });
        var closestBiome = ListHelpers.MinBy(matchingBiomesWithDifference, (a, b) => a.difference < b.difference).biome;

        // Sanity check and return.
#if UNITY_EDITOR
        Assert.IsNotNull(closestBiome, "There should always be at least one matching biome. Please review the logic here.");
#endif
        return closestBiome;
    }

    public GameObject InstantiateTile(int x, int y, WorldDisplayMode worldDisplayMode = WorldDisplayMode.ActualTiles)
    {
        // First, determine the closest biome.
        var biome = ClosestBiome(x, y);

        // Then given the biome, instantiate the appropriate tile.
        GameObject tile = null;
        if (biome == worldConfig.CopperOreBiome)
            tile = Object.Instantiate(worldConfig.CopperOrePrefab);
        else if (biome == worldConfig.CoalBiome)
            tile = Object.Instantiate(worldConfig.CoalPrefab);
        else if (biome == worldConfig.SugarCaneBiome)
            tile = Object.Instantiate(worldConfig.SugarCanePrefab);
        else if (biome == worldConfig.WheatBiome)
            tile = Object.Instantiate(worldConfig.WheatPrefab);
        else if (biome == worldConfig.StoneBiome)
            tile = Object.Instantiate(worldConfig.StonePrefabs[Random.Range(0, worldConfig.StonePrefabs.Length)]);
        else if (biome == worldConfig.IronOreBiome)
            tile = Object.Instantiate(worldConfig.IronOrePrefab);
        else if (biome == worldConfig.WaterBiome)
            tile = Object.Instantiate(worldConfig.WaterPrefab);
        else if (biome == worldConfig.WoodBiome)
            tile = Object.Instantiate(worldConfig.TreePrefab);
        else
        {
            return null;
            // Instantiate a tile.
            // tile = Object.Instantiate(worldConfig.PlaceholderPrefab);

            // Then given the biome, set the material of the tile.
            // var material = worldConfig.GetMaterial(biome);
            // if (material != null)
                // tile.GetComponentInChildren<MeshRenderer>().material = material;
        }

        // Set the tile's position.
        Vector3 position;
        if (biome == worldConfig.CopperOreBiome || biome == worldConfig.CoalBiome || biome == worldConfig.SugarCaneBiome || biome == worldConfig.WheatBiome || biome == worldConfig.StoneBiome || biome == worldConfig.IronOreBiome || biome == worldConfig.WaterBiome || biome == worldConfig.WoodBiome)
        {
            position = new Vector3(x - worldConfig.GridWidth / 2, 0f, y - worldConfig.GridHeight / 2);
        }
        else
        {
            position = new Vector3(x - worldConfig.GridWidth / 2, -.5f, y - worldConfig.GridHeight / 2);
        }
        if (biome == worldConfig.WaterBiome)
        {
            position += new Vector3(0f, .01f, 0f); // Water overlaps with adjacent tiles a little, so let's bump it up.
        }
        tile.transform.position = position;

        // For non-placeholder tiles, give them a random rotation.
        if (biome == worldConfig.CopperOreBiome || biome == worldConfig.CoalBiome || biome == worldConfig.SugarCaneBiome || biome == worldConfig.WheatBiome || biome == worldConfig.StoneBiome || biome == worldConfig.IronOreBiome || biome == worldConfig.WoodBiome)
        {
            tile.transform.GetChild(0).rotation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up);
        }

        // For trees, give it a random scale for some visual variety.
        if (biome == worldConfig.WoodBiome)
        {
            var scalingFactor = Random.Range(.25f, .5f);
            tile.GetComponent<TreeScaler>().SetScale(scalingFactor);
        }

        // Set the tile's scale if instantiating debug tiles.
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
            tile.transform.localScale = new Vector3(1f, scale * 20 /* Recall that the pivot is at the cube center, so scaling will extend downward as well. */, 1f);
        }

        // Return instantiated tile.
        return tile;
    }
}
