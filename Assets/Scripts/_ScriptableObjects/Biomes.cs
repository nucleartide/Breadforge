using UnityEngine;

[CreateAssetMenu]
public class Biomes : ScriptableObject
{
    public float HeightMin;
    public float HeightMax;
    public float HeatMin;
    public float HeatMax;
    public float MoistureMin;
    public float MoistureMax;

    [Header("land and earth")]
    public Biome waterBiome;

    public Biome groundBiome;

    // Below this line: TODO.

    [Header("stone and such")]
    public Biome stoneBiome;
    public Biome coalBiome;
    public Biome copperOreBiome;
    public Biome ironOreBiome;

    [Header("above ground")]
    public Biome woodBiome;
    public Biome sugarCaneBiome;
    public Biome wheatBiome;
}
