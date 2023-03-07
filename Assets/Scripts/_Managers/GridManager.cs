using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [System.Serializable]
    public enum WorldInstantiateMode
    {
        None,
        Height,
        Moisture,
        Heat,
    }

    public class NoiseMap
    {
        public float[,] Map;
        public float MinValue;
        public float MaxValue;
    }

    private List<Transform> DebugObjects;

    [SerializeField]
    [NotNull]
    private World world;

    [SerializeField]
    [NotNull]
    private Transform cubePrefab;

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

    // Start is called before the first frame update
    void Start()
    {
        throw System.Exception("continue refactoring grid");
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public bool Satisfies(Query query, World.Biome biome)
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
