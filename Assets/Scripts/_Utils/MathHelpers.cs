using UnityEngine;

#if UNITY_EDITOR
using UnityEngine.Assertions;
#endif

public static class MathHelpers
{
    /// <summary>
    /// Version of Mathf.Clamp that takes in a degree value and min/max bounds (which can be negative).
    ///
    /// It will then clamp the degree value to the min and max bounds, and return the clamped value.
    ///
    /// Use this for clamping rotations.
    /// </summary>
    public static float RotationClamp(float value, float min, float max)
    {
#if UNITY_EDITOR
        Assert.IsTrue(min > -180f);
        Assert.IsTrue(max < 180f);
        Assert.IsTrue(min < max);
#endif

        if (value < 0f) value += 360f;
        if (min < 0f) min += 360f;
        if (max < 0f) max += 360f;

        if (value > 180f && value < min)
            value = min;
        else if (value < 180f && value > max)
            value = max;

        return value;
    }
}
