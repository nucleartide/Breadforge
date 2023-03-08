using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private WorldConfiguration worldConfig;

    [SerializeField]
    private WorldDisplayMode worldDisplayMode;

    [SerializeField]
    [NotNull]
    private GameObject tilePrefab;

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
                var tile = Instantiate(tilePrefab);
                worldMap.InitializeTile(tile, worldDisplayMode, x, y);
                tiles.Add(tile);
            }
        }

        return tiles;
    }

    public void RegenerateTiles()
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

    private void Start()
    {
        RegenerateTiles();
    }
}
