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
}
