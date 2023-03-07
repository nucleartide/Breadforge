using UnityEngine;

[CreateAssetMenu]
public class World : ScriptableObject
{
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

    [Header("Land and Water")]
    [SerializeField]
    private Biome waterBiome;

    [SerializeField]
    private Biome landBiome;

    [Header("Bedrock")]
    [SerializeField]
    private Biome stoneBiome;

    [SerializeField]
    private Biome coalBiome;

    [SerializeField]
    private Biome copperOreBiome;

    [SerializeField]
    private Biome ironOreBiome;

    [Header("Vegetation")]
    [SerializeField]
    private Biome woodBiome;

    [SerializeField]
    private Biome sugarCaneBiome;

    [SerializeField]
    private Biome wheatBiome;

    /// <summary>
    /// The height, heat, and moisture values are between [0,1], so
    /// a value of -1 is a safe value for indicating uninitialization.
    /// </summary>
    private const float UNINITIALIZED = -1f;
    private float heightMin = UNINITIALIZED;
    private float heightMax = UNINITIALIZED;
    private float heatMin = UNINITIALIZED;
    private float heatMax = UNINITIALIZED;
    private float moistureMin = UNINITIALIZED;
    private float moistureMax = UNINITIALIZED;

    public void Initialize(float heightMin, float heightMax, float heatMin, float heatMax, float moistureMin, float moistureMax)
    {
        this.heightMin = heightMin;
        this.heightMax = heightMax;
        this.heatMin = heatMin;
        this.heatMax = heatMax;
        this.moistureMin = moistureMin;
        this.moistureMax = moistureMax;
    }

    public struct Query
    {
        public float Height;
        public float Moisture;
        public float Heat;
    }

    /// <summary>
    /// Evaluate whether a set of (Height, Moisture, Heat) values satisfies a biome.
    /// </summary>
    public bool Satisfies(Query query, Biome biome)
    {
#if UNITY_EDITOR
        if (heightMin == UNINITIALIZED)
            throw new System.Exception("BiomeManager is uninitialized. Did you forget to call `.Initialize()`?");
#endif

        var normalizedMinHeight = heightMin + biome.MinHeight * (heightMax - heightMin);
        var normalizedMinMoisture = moistureMin + biome.MinMoisture * (moistureMax - moistureMin);
        var normalizedMinHeat = heatMin + biome.MinHeat * (heatMax - heatMin);
        return query.Height >= normalizedMinHeight && query.Moisture >= normalizedMinMoisture && query.Heat >= normalizedMinHeat;
    }
}
