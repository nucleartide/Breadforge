using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class WorldConfig : ScriptableObject
{
    [Header("Grid Dimensions")]
    public int GridWidth = 100;

    [SerializeField]
    public int GridHeight = 100;

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

    public List<Wave> HeatMap = new List<Wave>
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

    private List<Biome> AllBiomes
    {
        get
        {
            return new List<Biome>
            {
                landBiome,
                waterBiome,
                stoneBiome,
                coalBiome,
                copperOreBiome,
                ironOreBiome,
                woodBiome,
                sugarCaneBiome,
                wheatBiome,
            };
        }
    }

    public List<Biome> FindMatchingBiomes(Query query)
    {
        return AllBiomes.FindAll(biome => query.Satisfies(biome));
    }
}
