using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectOld : MonoBehaviour
{
    float collectionRate = 3.0f; // 1 collection every 3 seconds, let's try it out

    [SerializeField]
    [NotNull]
    TileEventManager tileEventManager;

    [SerializeField]
    [NotNull]
    PlayerInventory playerInventory;

    Coroutine currentlyCollectedResource = null;
    TileQuantity currentlyCollectedTile = null;

    void OnTileClick(TileQuantity tileQuantity)
    {
        Debug.Log("on tile click");
        currentlyCollectedResource = StartCoroutine(CollectResource(tileQuantity, collectionRate, playerInventory));
        currentlyCollectedTile = tileQuantity;
    }

    void OnTileMouseLeave(TileQuantity tileQuantity)
    {
        if (tileQuantity == currentlyCollectedTile)
        {
            if (currentlyCollectedResource != null)
                StopCoroutine(currentlyCollectedResource);
            currentlyCollectedResource = null;
            currentlyCollectedTile = null;
        }
    }

    void OnTileRelease(TileQuantity tileQuantity)
    {
        if (currentlyCollectedResource != null)
            StopCoroutine(currentlyCollectedResource);
        currentlyCollectedResource = null;
        currentlyCollectedTile = null;
    }

    /// <summary>
    /// don't technically need a coroutine, but it encapsulates the elapsedTime variable nicely.
    /// </summmary>
    IEnumerator CollectResource(TileQuantity tileQuantity, float collectionRate, PlayerInventory playerInventory)
    {
        var elapsedTime = 0f;
        while (true)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > collectionRate)
            {
                // then add inventory
                playerInventory.AddInventory(tileQuantity);

                // clear elapsed time in order to repeat
                elapsedTime = 0f;
            }

            // done for this frame.
            yield return null;
        }
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        tileEventManager.OnTileClicked += OnTileClick;
        tileEventManager.OnTileMouseLeave += OnTileMouseLeave;
        tileEventManager.OnTileReleased += OnTileRelease;

        // x when a click is received, kick off a coroutine
        // x when enough time has elapsed, add the inventory
        // x repeat: when enough time has elapsed, add the inventory
        // x if releasing mouse, stop the coroutine and clear local state
        // x if mouse left the tile, stop the coroutine and clear local state
    }

    void OnDisable()
    {
        tileEventManager.OnTileClicked -= OnTileClick;
        tileEventManager.OnTileMouseLeave -= OnTileMouseLeave;
        tileEventManager.OnTileReleased -= OnTileRelease;
    }
}
