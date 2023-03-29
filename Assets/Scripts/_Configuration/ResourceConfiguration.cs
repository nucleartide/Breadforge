using UnityEngine;

/// <summary>
/// Configuration for a specific resource type.
/// </summary>
[CreateAssetMenu]
public class ResourceConfiguration : ScriptableObject
{
    /// <summary>
    /// The number of seconds that must elapse before CollectedQuantity is awarded to the player.
    /// </summary>

    /// <summary>
    /// The number of seconds that must elapse before CollectedQuantity is awarded to the player.
    /// </summary>
    [field: SerializeField]
    public float TimeToCollect
    {
        get;
        private set;
    }

    /// <summary>
    /// The quantity that is collected after TimeToCollect has elapsed.
    /// </summary>
    [field: SerializeField]
    public float CollectedQuantity
    {
        get;
        private set;
    }

    /// <summary>
    /// The initial quantity of the resource, specified as a multiple of CollectedQuantity.
    /// </summary>
    [field: SerializeField]
    public int NumCollections
    {
        get;
        private set;
    }

    /// <summary>
    /// The animation that should be played when the player interacts with this resource.
    /// </summary>
    [field: SerializeField]
    [field: NotNull]
    public StateEnumValue PlayerStateEnum
    {
        get;
        set;
    }

    public float InitialQuantity
    {
        get
        {
            return NumCollections * CollectedQuantity;
        }
    }
}
