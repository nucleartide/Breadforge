using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    Dictionary<Tile, int> inventory = new Dictionary<Tile, int>();

    // [SerializeField]
    // [NotNull]
    // TileEventManager tileEventManager;

    public void AddInventory(TileQuantity tileQuantity)
    {
        var tile = tileQuantity.Tile;
        if (inventory.ContainsKey(tile))
            inventory[tile] += tileQuantity.Quantity;
        else
            inventory[tile] = tileQuantity.Quantity;
        Debug.Log("updated inventory is " + inventory[tile] + " " + tile.Label);
    }
}
