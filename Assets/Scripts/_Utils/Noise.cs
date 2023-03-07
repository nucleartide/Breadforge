using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    /// <param name="width">Width of the generated map.</param>
    /// <param name="height">Height of the generated map.</param>
    /// <param name="stackOfWaves">The stack of waves to combine into one.</param>
    /// <param name="scale">How zoomed-in the map will be. Pass in a value of 1.0 for no zooming.</param>
    /// <param name="offset">The offset used when sampling from Perlin noise. Pass in a value of Vector2.zero for no offset.</param>
    public static NoiseMap GenerateMap(int width, int height, List<Wave> stackOfWaves, float scale, Vector2 offset)
    {
        var noiseMap = new float[height, width];
        var minValue = float.MaxValue;
        var maxValue = float.MinValue;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var sampleX = x * scale + offset.x;
                var sampleY = y * scale + offset.y;
                var normalization = 0.0f; // Needed to normalize to [0,1] after summing the stack of noise values.

                foreach (var wave in stackOfWaves)
                {
                    noiseMap[y, x] += wave.Amplitude * Mathf.Clamp01(Mathf.PerlinNoise(sampleX * wave.Frequency + wave.Seed, sampleY * wave.Frequency + wave.Seed));
                    normalization += wave.Amplitude;
                }

                noiseMap[y, x] /= normalization;
                if (noiseMap[y, x] < minValue)
                    minValue = noiseMap[y, x];
                if (noiseMap[y, x] > maxValue)
                    maxValue = noiseMap[y, x];
            }
        }

        return new NoiseMap
        {
            Map = noiseMap,
            MinValue = minValue,
            MaxValue = maxValue,
        };
    }
}
