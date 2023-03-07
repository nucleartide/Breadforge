using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private WorldConfig worldConfig;

    [SerializeField]
    private WorldInstantiateMode worldInstantiateMode;

    private WorldMap worldMap;

    private List<GameObject> tiles;

    private void Start()
    {
        // Destroy the world map if one exists.
        // ...

        // Construct a world map.
        worldMap = new WorldMap(worldConfig);

        // Instantiate the world map.
        // ...
    }
}
