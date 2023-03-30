using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private WorldConfiguration worldConfig;

    [SerializeField]
    private WorldDisplayMode worldDisplayMode;

    private WorldMap worldMap;

    private List<GameObject> tiles;

    private List<GameObject> InstantiateTiles()
    {
        var gridHeight = worldConfig.GridHeight;
        var gridWidth = worldConfig.GridWidth;
        var tiles = new List<GameObject>();

        for (var y = 0; y < gridHeight; y++)
        {
            for (var x = 0; x < gridWidth; x++)
            {
                var tile = worldMap.InstantiateTile(x, y, worldDisplayMode);
                tiles.Add(tile);
            }
        }

        return tiles;
    }

    public void RegenerateResources()
    {
        // Destroy if an existing set of tiles exists.
        if (tiles != null)
            foreach (var tile in tiles)
                Destroy(tile);

        // Construct a world map.
        worldMap = new WorldMap(worldConfig);

        // Instantiate the world map.
        tiles = InstantiateTiles();
    }

    public void OnEnable() => RegenerateResources();
}
