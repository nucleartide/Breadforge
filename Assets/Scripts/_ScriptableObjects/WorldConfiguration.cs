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
    public List<Wave> HeightMapConfig = new List<Wave>
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

    public List<Wave> MoistureMapConfig = new List<Wave>
    {
        new Wave {
            Seed = 621f,
            Frequency = .03f,
            Amplitude = 1f,
        },
    };

    public List<Wave> HeatMapConfig = new List<Wave>
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

    [Header("Biomes: Land and Water")]
    [SerializeField]
    [NotNull]
    private Biome landBiome;

    [SerializeField]
    [NotNull]
    private Biome waterBiome;

    [Header("Biomes: Bedrock")]
    [SerializeField]
    [NotNull]
    private Biome stoneBiome;

    [SerializeField]
    [NotNull]
    private Biome coalBiome;

    [SerializeField]
    [NotNull]
    private Biome copperOreBiome;

    [SerializeField]
    [NotNull]
    private Biome ironOreBiome;

    [Header("Biomes: Vegetation")]
    [SerializeField]
    [NotNull]
    private Biome woodBiome;

    [SerializeField]
    [NotNull]
    private Biome sugarCaneBiome;

    [SerializeField]
    [NotNull]
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
