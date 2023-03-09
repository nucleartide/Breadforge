using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TileEventManager : ScriptableObject
{
    public delegate void TileClick(TileQuantity tileQuantity);
    public event TileClick OnTileClicked;

    public delegate void TileRelease(TileQuantity tileQuantity);
    public event TileRelease OnTileReleased;

    public delegate void TileMouseLeave(TileQuantity tileQuantity);
    public event TileMouseLeave OnTileMouseLeave;

    public void ClickTile(TileQuantity tileQuantity)
    {
        OnTileClicked?.Invoke(tileQuantity);
    }

    public void ReleaseTile(TileQuantity tileQuantity)
    {
        OnTileReleased?.Invoke(tileQuantity);
    }

    public void MouseLeaveTile(TileQuantity tileQuantity)
    {
        OnTileMouseLeave?.Invoke(tileQuantity);
    }
}
