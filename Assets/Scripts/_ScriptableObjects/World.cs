using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu]
public class World : ScriptableObject
{
    [Serializable]
    public struct Wave
    {
        public float Seed;
        public float Frequency;
        public float Amplitude;
    }

    [System.Serializable]
    public struct Biome
    {
        [Tooltip("Ranges from 0 to 1.")]
        public float MinHeight;

        [Tooltip("Ranges from 0 to 1.")]
        public float MinMoisture;

        [Tooltip("Ranges from 0 to 1.")]
        public float MinHeat;
    }

    [Header("Grid Dimensions")]
    [SerializeField]
    private int GridWidth = 100;

    [SerializeField]
    private int GridHeight = 100;

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
}
