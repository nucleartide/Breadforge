using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListHelpers
{
    /// <summary>
    /// Function that takes in a list of stuff and a comparator function, and
    /// returns the minimum value in the list as determined by the comparator function.
    /// </summary>
    public static T MinBy<T>(List<T> stuff, System.Func<T, T, bool> comparator)
    {
        if (stuff.Count == 0)
            return default(T);

        var minT = stuff[0];
        for (var i = 1; i < stuff.Count; i++)
        {
            var elem = stuff[i];
            if (comparator(elem, minT))
                minT = elem;
        }

        return minT;
    }
}
