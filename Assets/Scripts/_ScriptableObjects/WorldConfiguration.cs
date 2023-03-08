using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class WorldConfiguration : ScriptableObject
{
    [Header("Grid Dimensions")]
    public int GridWidth = 100;

    [SerializeField]
    public int GridHeight = 100;

    [Header("Materials")]
    [SerializeField]
    [NotNull]
    private ResourceMaterialManager materialManager;

    [Header("Noise Maps")]
    public List<NoiseWave> HeightMapConfig = new List<NoiseWave>
    {
        new NoiseWave {
            Seed = 56f,
            Frequency = .05f,
            Amplitude = 1f,
        },
        new NoiseWave {
            Seed = 199.36f,
            Frequency = .1f,
            Amplitude = .5f,
        },
    };

    public List<NoiseWave> MoistureMapConfig = new List<NoiseWave>
    {
        new NoiseWave {
            Seed = 621f,
            Frequency = .03f,
            Amplitude = 1f,
        },
    };

    public List<NoiseWave> HeatMapConfig = new List<NoiseWave>
    {
        new NoiseWave {
            Seed = 318.6f,
            Frequency = .04f,
            Amplitude = 1f,
        },
        new NoiseWave {
            Seed = 329.7f,
            Frequency = .02f,
            Amplitude = .5f,
        },
    };

    [Header("Biomes: Land and Water")]
    [SerializeField]
    private Biome landBiome;

    [SerializeField]
    private Biome waterBiome;

    [Header("Biomes: Bedrock")]
    [SerializeField]
    private Biome stoneBiome;

    [SerializeField]
    private Biome coalBiome;

    [SerializeField]
    private Biome copperOreBiome;

    [SerializeField]
    private Biome ironOreBiome;

    [Header("Biomes: Vegetation")]
    [SerializeField]
    private Biome woodBiome;

    [SerializeField]
    private Biome sugarCaneBiome;

    [SerializeField]
    private Biome wheatBiome;

    public List<Biome> AllBiomes
    {
        get
        {
            return new List<Biome>
            {
                // land and water
                landBiome,
                waterBiome,

                // bedrock
                stoneBiome,
                coalBiome,
                copperOreBiome,
                ironOreBiome,

                // vegetation
                woodBiome,
                sugarCaneBiome,
                wheatBiome,
            };
        }
    }

    private static Dictionary<Biome, Material> materialLookupSingleton;
    private Dictionary<Biome, Material> materialLookup
    {
        get
        {
            if (materialLookupSingleton != null)
                return materialLookupSingleton;

            return new Dictionary<Biome, Material>
            {
                {waterBiome, materialManager.Water},
                {coalBiome, materialManager.Coal},
                {copperOreBiome, materialManager.CopperOre},
                {woodBiome, materialManager.Wood},
                {ironOreBiome, materialManager.IronOre},
                {stoneBiome, materialManager.Stone},
                {sugarCaneBiome, materialManager.SugarCane},
                {wheatBiome, materialManager.Wheat},
            };
        }
    }

    /// <summary>
    /// Given a biome, fetch the biome's corresponding material.
    /// </summary>
    public Material GetMaterial(Biome biome)
    {
        if (!materialLookup.ContainsKey(biome))
            return null;

        return materialLookup[biome];
    }

    public List<Biome> FindMatchingBiomes(Query query)
    {
        return AllBiomes.FindAll(biome => query.Satisfies(biome));
    }
}
