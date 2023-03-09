using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class WorldConfiguration : ScriptableObject
{
    [Header("Grid Dimensions")]
    public int GridWidth = 100;

    [SerializeField]
    public int GridHeight = 100;

    [Header("Resource Prefabs")]
    [SerializeField]
    [NotNull]
    public GameObject PlaceholderPrefab;

    [SerializeField]
    [NotNull]
    private PlaceholderMaterials placeholderMaterials;

    [field: SerializeField]
    [field: NotNull]
    public GameObject CopperOrePrefab
    {
        get;
        private set;
    }

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

    [field: Header("Biomes: Land and Water")]
    [field: SerializeField]
    public Biome LandBiome
    {
        get;
        private set;
    }

    [field: SerializeField]
    public Biome WaterBiome
    {
        get;
        private set;
    }

    [field: Header("Biomes: Bedrock")]
    [field: SerializeField]
    public Biome StoneBiome
    {
        get;
        private set;
    }

    [field: SerializeField]
    public Biome CoalBiome
    {
        get;
        private set;
    }

    [field: SerializeField]
    public Biome CopperOreBiome
    {
        get;
        private set;
    }

    [field: SerializeField]
    public Biome IronOreBiome
    {
        get;
        private set;
    }

    [field: Header("Biomes: Vegetation")]
    [field: SerializeField]
    public Biome WoodBiome
    {
        get;
        private set;
    }

    [field: SerializeField]
    public Biome SugarCaneBiome
    {
        get;
        private set;
    }

    [field: SerializeField]
    public Biome WheatBiome
    {
        get;
        private set;
    }

    public List<Biome> AllBiomes
    {
        get
        {
            return new List<Biome>
            {
                // land and water
                LandBiome,
                WaterBiome,

                // bedrock
                StoneBiome,
                CoalBiome,
                CopperOreBiome,
                IronOreBiome,

                // vegetation
                WoodBiome,
                SugarCaneBiome,
                WheatBiome,
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
                {WaterBiome, placeholderMaterials.Water},
                {CoalBiome, placeholderMaterials.Coal},
                {CopperOreBiome, placeholderMaterials.CopperOre},
                {WoodBiome, placeholderMaterials.Wood},
                {IronOreBiome, placeholderMaterials.IronOre},
                {StoneBiome, placeholderMaterials.Stone},
                {SugarCaneBiome, placeholderMaterials.SugarCane},
                {WheatBiome, placeholderMaterials.Wheat},
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
