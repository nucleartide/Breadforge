using UnityEngine;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Selects one tile to instantiate from a set of tiles.
/// </summary>
public class GroundTileRandomizer : MonoBehaviour
{
    [System.Serializable]
    private class WeightedPrefab
    {
        public GameObject Prefab;
        public float Weight;
    }

    [SerializeField]
    private List<WeightedPrefab> tilePrefabs;

    private static List<Quaternion> tileRotations = new List<Quaternion>
    {
        Quaternion.AngleAxis(0, Vector3.up),
        Quaternion.AngleAxis(90, Vector3.up),
        Quaternion.AngleAxis(180, Vector3.up),
        Quaternion.AngleAxis(270, Vector3.up),
    };

    void Start()
    {
        var randomTile = ListHelpers.Choose(
            tilePrefabs.Select(tag => tag.Prefab).ToList(),
            tilePrefabs.Select(tag => tag.Weight).ToList());
        var tile = Instantiate(randomTile);
        tile.transform.SetParent(gameObject.transform);
        tile.transform.SetLocalPositionAndRotation(Vector3.zero, ListHelpers.Random(tileRotations));
    }
}
