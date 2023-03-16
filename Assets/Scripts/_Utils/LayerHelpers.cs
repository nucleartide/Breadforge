using UnityEngine;

public static class LayerHelpers
{
    public static int CollectibleResource
    {
        get
        {
            return LayerMask.GetMask("Collectible Resource");
        }
    }
}
