using UnityEngine;

[CreateAssetMenu]
public class TileQuantity : ScriptableObject
{
    [field: SerializeField]
    public Tile Tile
    {
        get;
        private set;
    }

    [field: SerializeField]
    public int Quantity
    {
        get;
        private set;
    }

    public TileQuantity Initialize(int newQuantity, Tile newTile)
    {
        Quantity = newQuantity;
        Tile = newTile;
        return this;
    }
}
