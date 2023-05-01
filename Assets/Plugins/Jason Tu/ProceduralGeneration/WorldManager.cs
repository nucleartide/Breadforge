using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private WorldConfiguration worldConfig;

    [SerializeField]
    private WorldDisplayMode worldDisplayMode;

    [SerializeField]
    [NotNull(IgnorePrefab = true)]
    private UnityEngine.Tilemaps.Tilemap tilemap;

    [SerializeField]
    [NotNull(IgnorePrefab = true)]
    private UnityEngine.Tilemaps.Tilemap grassTilemap;

    [SerializeField]
    [NotNull]
    private UnityEngine.Tilemaps.TileBase groundRuleTile;

    [SerializeField]
    [NotNull]
    private UnityEngine.Tilemaps.TileBase waterRuleTile;

    [SerializeField]
    [NotNull]
    private UnityEngine.Tilemaps.TileBase grassRuleTile;

    private WorldMap worldMap;

    private List<GameObject> tiles;

    private List<GameObject> InstantiateTiles()
    {
        var gridHeight = worldConfig.GridHeight;
        var gridWidth = worldConfig.GridWidth;
        var r = gridWidth * .5f;
        var tiles = new List<GameObject>();

        for (var y = 0; y < gridHeight; y++)
        {
            for (var x = 0; x < gridWidth; x++)
            {
                var xh = x - r;
                var yk = y - r;
                if (xh * xh + yk * yk <= r * r)
                {
                    var isWaterBiome = worldConfig.IsWaterBiome(worldMap.ClosestBiome(x, y));
                    if (!isWaterBiome)
                    {
                        var tile = worldMap.InstantiateTile(x, y, worldDisplayMode);
                        tiles.Add(tile);
                    }
                }
            }
        }

        return tiles;
    }

    private bool ShouldSetGroundTile(int x, int y)
    {
        var isGroundBiome = worldMap.ClosestBiome(x, y, Query.QueryType.GroundBiomesOnly) != worldConfig.WaterBiome;
        if (isGroundBiome)
            return true;

        for (var i = -1; i <= 1; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                if (worldMap.ClosestBiome(x + i, y + j, Query.QueryType.GroundBiomesOnly) != worldConfig.WaterBiome)
                    return true;
            }
        }

        return false;
    }

    private bool ShouldSetGrassTile(int x, int y)
    {
        var isGroundBiome = worldMap.ClosestBiome(x, y, Query.QueryType.GroundBiomesOnly) == worldConfig.GrassBiome;
        if (isGroundBiome)
            return true;

        for (var i = -1; i <= 1; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                if (worldMap.ClosestBiome(x + i, y + j, Query.QueryType.GroundBiomesOnly) == worldConfig.GrassBiome)
                    return true;
            }
        }

        return false;
    }

    private void InstantiateTilemap()
    {
        var gridHeight = worldConfig.GridHeight;
        var gridWidth = worldConfig.GridWidth;
        var r = gridWidth * .5f;

        // Starting at 1 to get rid of these strange pointy circle bits...
        for (var y = 1; y < gridHeight; y++)
        {
            for (var x = 1; x < gridWidth; x++)
            {
                var xh = x - r;
                var yk = y - r;
                var sumOfSquares = xh * xh + yk * yk;

                if (sumOfSquares <= r * r)
                {
                    // Select tile.
                    var tile = ShouldSetGroundTile(x, y) ? groundRuleTile : waterRuleTile;

                    // Set tite.
                    tilemap.SetTile(new Vector3Int((int)(x - r), (int)(y - r), 0), tile);

                    // Update grass tilemap separately.
                    if (ShouldSetGrassTile(x, y))
                        grassTilemap.SetTile(new Vector3Int((int)(x - r), (int)(y - r), 0), grassRuleTile);
                }
            }
        }

    }

    private void DestroyTilemap(UnityEngine.Tilemaps.Tilemap tilemap)
    {
        var childCount = tilemap.transform.childCount;
        for (var i = 0; i < childCount; i++)
            Destroy(tilemap.transform.GetChild(i).gameObject);

        tilemap.ClearAllTiles();
    }

    public void RegenerateResources()
    {
        // Destroy if an existing set of tiles exists.
        if (tiles != null)
            foreach (var tile in tiles)
                Destroy(tile);

        // Clear tilemaps.
        DestroyTilemap(tilemap);
        DestroyTilemap(grassTilemap);

        // Construct a world map.
        worldMap = new WorldMap(worldConfig);

        // Instantiate the world map.
        tiles = InstantiateTiles();

        // Instantiate the tilemap as well.
        InstantiateTilemap();
    }

    public void OnEnable() => RegenerateResources();
}
