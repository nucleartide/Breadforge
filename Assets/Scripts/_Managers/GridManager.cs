using UnityEngine;
using System;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
#if false
    public enum GridMode
    {
        DebugHeightMap,
        DebugMoistureMap,
        DebugHeatMap,
        InstantiateTiles,
    }

    [SerializeField]
    TileUI tileUIPrefab;

    [SerializeField]
    GridMode gridMode = GridMode.InstantiateTiles;

    float[,] heightMap;
    float[,] moistureMap;
    float[,] heatMap;
    List<TileUI> grid = new List<TileUI>();

    [SerializeField]
    [NotNull]
    TileEventManager tileEventManager;

    [SerializeField]
    [NotNull]
    Tile coal;

    [SerializeField]
    [NotNull]
    Tile stone;

    [SerializeField]
    [NotNull]
    Tile ironOre;

    [SerializeField]
    [NotNull]
    Tile copperOre;

    [SerializeField]
    [NotNull]
    Tile wheat;

    [SerializeField]
    [NotNull]
    Tile sugarCane;

    [SerializeField]
    [NotNull]
    Tile waterPack;

    [SerializeField]
    GameObject genericCube;

    public static Biome ClosestBiome(List<Biome> biomePresets, float[,] heightMap, float[,] moistureMap, float[,] heatMap, int x, int y)
    {
        float height = heightMap[y, x];
        float moisture = moistureMap[y, x];
        float heat = heatMap[y, x];

        // Find all the matching biomes.
        var matchingBiomes = biomePresets.FindAll(biome => biome.MatchCondition(height, moisture, heat));
if (matchingBiomes.Count == 0)
{
return null;
}

foreach (var biome in matchingBiomes)
{
}

        // For each matching biome, find the difference value.
        var matchingBiomesWithDifference = matchingBiomes.ConvertAll(biome =>
        {
            return (biome, (height - biome.MinHeight) + (moisture - biome.MinMoisture) + (heat - biome.MinHeat));
        });

        // Find the biome with the minimum difference value. It seems this version of LINQ has no MinBy.
        var minValue = 1000f;
        Biome minBiome = null;
        foreach (var thing in matchingBiomesWithDifference)
        {
        }
        foreach (var (biome, difference) in matchingBiomesWithDifference)
        {
            Debug.Log("difference " + difference);
            if (difference < minValue)
            {
                Assert.IsNotNull(biome);
                minValue = difference;
                minBiome = biome;
            }
        }

        // Sanity check and return.
        Assert.IsNotNull(minBiome);
        return minBiome;
    }

    void Start()
    {

        var biomePresets = new List<Biome>()
        {
            waterPackBiome,
            groundBiome,
            stoneBiome,
            coalBiome,
            ironOreBiome,
            copperOreBiome,
            wheatBiome,
            sugarCaneBiome,
        };

        var minHeight = float.MaxValue;
        var maxHeight = float.MinValue;

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                if (gridMode == GridMode.InstantiateTiles)
                {
                    // Instantiate tile based on closest biome.
                    var biome = ClosestBiome(biomePresets, heightMap, moistureMap, heatMap, x, y);
                    if (biome == groundBiome || biome == null)
                        continue;
                    // TODO: change closest biome algorithm. I want to have pockets of resources.

                    var tileUI = Instantiate(tileUIPrefab);
                    var position = new Vector3(x - gridWidth / 2, .01f, y - gridHeight / 2);
                    var tileQuantity = ScriptableObject.CreateInstance<TileQuantity>();
                    if (biome == coalBiome)
                        tileQuantity.Initialize(20, coal);
                    else if (biome == copperOreBiome)
                        tileQuantity.Initialize(20, copperOre);
                    else if (biome == ironOreBiome)
                        tileQuantity.Initialize(20, ironOre);
                    else if (biome == stoneBiome)
                        tileQuantity.Initialize(20, stone);
                    else if (biome == sugarCaneBiome)
                        tileQuantity.Initialize(20, sugarCane);
                    else if (biome == waterPackBiome)
                        tileQuantity.Initialize(20, waterPack);
                    else if (biome == wheatBiome)
                        tileQuantity.Initialize(20, wheat);
                    else
                        throw new System.Exception("Unimplemented");
                    tileUI.Initialize(tileQuantity, position, tileEventManager);
                    grid.Add(tileUI);
                }
                else if (gridMode == GridMode.DebugHeightMap)
                {
                    var cube = Instantiate(genericCube);
                    var debugUI = cube.GetComponent<DebugUI>();
                    var position = new Vector3(x - gridWidth / 2, .01f, y - gridHeight / 2);
                    var scale = heightMap[y, x];
                    debugUI.SetText($"scale: {scale}", new Color32((byte)(scale * 255), (byte)(scale * 255), (byte)(scale * 255), 255));
                    cube.transform.position = position;
                    cube.transform.localScale = new Vector3(1f, scale * 20 /* Recall that the pivot is at the cube center, so scaling will extend downward as well. */, 1f);

                    if (scale < minHeight) minHeight = scale;
                    if (scale > maxHeight) maxHeight = scale;
                }
                else if (gridMode == GridMode.DebugMoistureMap)
                {
                    var cube = Instantiate(genericCube);
                    var debugUI = cube.GetComponent<DebugUI>();
                    var position = new Vector3(x - gridWidth / 2, .01f, y - gridHeight / 2);
                    var scale = moistureMap[y, x];
                    debugUI.SetText($"scale: {scale}", new Color32((byte)(scale * 255), (byte)(scale * 255), (byte)(scale * 255), 255));
                    cube.transform.position = position;
                    cube.transform.localScale = new Vector3(1f, scale * 20 /* Recall that the pivot is at the cube center, so scaling will extend downward as well. */, 1f);

                    if (scale < minHeight) minHeight = scale;
                    if (scale > maxHeight) maxHeight = scale;
                }
                else if (gridMode == GridMode.DebugHeatMap)
                {
                    var cube = Instantiate(genericCube);
                    var debugUI = cube.GetComponent<DebugUI>();
                    var position = new Vector3(x - gridWidth / 2, .01f, y - gridHeight / 2);
                    var scale = heatMap[y, x];
                    debugUI.SetText($"scale: {scale}", new Color32((byte)(scale * 255), (byte)(scale * 255), (byte)(scale * 255), 255));
                    cube.transform.position = position;
                    cube.transform.localScale = new Vector3(1f, scale * 20 /* Recall that the pivot is at the cube center, so scaling will extend downward as well. */, 1f);

                    if (scale < minHeight) minHeight = scale;
                    if (scale > maxHeight) maxHeight = scale;
                }

            }
        }

        Debug.Log($"minHeight: {minHeight}");
        Debug.Log($"maxHeight: {maxHeight}");
    }
#endif

    [Serializable]
    private struct Wave
    {
        public float Seed;
        public float Frequency;
        public float Amplitude;
    }

    [Serializable]
    private struct NoiseMapConfig
    {
        public List<Wave> Waves;
    }

    [SerializeField]
    private int gridWidth = 100;

    [SerializeField]
    private int gridHeight = 100;

    [SerializeField]
    private NoiseMapConfig heightMapConfig;

    [SerializeField]
    private NoiseMapConfig moistureMapConfig;

    [SerializeField]
    private NoiseMapConfig heatMapConfig;

    [SerializeField]
    Biome coalBiome;

    [SerializeField]
    Biome copperOreBiome;

    [SerializeField]
    Biome groundBiome;

    [SerializeField]
    Biome woodBiome;

    [SerializeField]
    Biome ironOreBiome;

    [SerializeField]
    Biome stoneBiome;

    [SerializeField]
    Biome sugarCaneBiome;

    [SerializeField]
    Biome waterBiome;

    [SerializeField]
    Biome wheatBiome;

    private float[,] heightMap;
    private float[,] moistureMap;
    private float[,] heatMap;

    /// <param name="width">Width of the generated map.</param>
    /// <param name="height">Height of the generated map.</param>
    /// <param name="stackOfWaves">The stack of waves to combine into one.</param>
    /// <param name="scale">How zoomed-in the map will be. Pass in a value of 1.0 for no zooming.</param>
    /// <param name="offset">The offset used when sampling from Perlin noise. Pass in a value of Vector2.zero for no offset.</param>
    private static float[,] GenerateNoiseMap(int width, int height, List<Wave> stackOfWaves, float scale, Vector2 offset)
    {
        var noiseMap = new float[height, width];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var sampleX = x * scale + offset.x;
                var sampleY = y * scale + offset.y;
                var normalization = 0.0f; // Needed to normalize to [0,1] after summing the stack of noise values.

                foreach (var wave in stackOfWaves)
                {
                    noiseMap[x, y] += wave.Amplitude * Mathf.PerlinNoise(sampleX * wave.Frequency + wave.Seed, sampleY * wave.Frequency + wave.Seed);
                    normalization += wave.Amplitude;
                }

                noiseMap[x, y] /= normalization;
            }
        }

        return noiseMap;
    }

    void Start()
    {
        heightMap = GenerateNoiseMap(gridWidth, gridHeight, heightMapConfig.Waves, 1f, Vector2.zero);
        moistureMap = GenerateNoiseMap(gridWidth, gridHeight, moistureMapConfig.Waves, 1f, Vector2.zero);
        heatMap = GenerateNoiseMap(gridWidth, gridHeight, heatMapConfig.Waves, 1f, Vector2.zero);
        Debug.Log("Maps have been generated.");
    }
}
