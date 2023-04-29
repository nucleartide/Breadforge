using System.Collections.Generic;

public static class ListHelpers
{
    /// <summary>
    /// Function that takes in a list of things and a comparator function, and
    /// returns the minimum value in the list as determined by the comparator function.
    ///
    /// Use this because C# in Unity 2021 doesn't have LINQ's MinBy function. :(
    /// </summary>
    public static T MinBy<T>(List<T> things, System.Func<T, T, bool> comparator)
    {
        if (things.Count == 0)
            return default(T);

        var minT = things[0];
        for (var i = 1; i < things.Count; i++)
        {
            var element = things[i];
            if (comparator(element, minT))
                minT = element;
        }

        return minT;
    }

    public static T Random<T>(List<T> things)
    {
        return things[UnityEngine.Random.Range(0, things.Count)];
    }

    /// <summary>
    /// Similar to Random(), but allows you to customize the weight between choices.
    /// </summary>
    public static T Choose<T>(List<T> things, List<float> weights)
    {
        var thresholds = new List<float>();
        var runningSum = 0f;

        // Compute list of thresholds.
        for (var i = 0; i < things.Count; i++)
        {
            thresholds.Add(runningSum);
            runningSum += weights[i];
        }

        // Normalize thresholds to [0, 1].
        for (var i = 0; i < thresholds.Count; i++)
        {
            thresholds[i] /= runningSum;
        }

        // Compute a random t value.
        var t = UnityEngine.Random.Range(0f, 1f);

        // If t meets or exceeds a threshold, return that threshold's corresponding thing.
        for (var i = thresholds.Count - 1; i >= 0; i--)
        {
            if (t >= thresholds[i])
                return things[i];
        }

        throw new System.Exception("Huh, this shouldn't happen.");
    }
}
