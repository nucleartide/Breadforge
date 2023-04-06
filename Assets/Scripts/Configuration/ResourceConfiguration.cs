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
    /// NumCollections * CollectedQuantity == InitialQuantity.
    /// </summary>
    [field: SerializeField]
    public int NumCollections
    {
        get;
        private set;
    }

    /// <summary>
    /// The initial quantity of the resource, specified as a multiple of CollectedQuantity.
    /// </summary>
    public float InitialQuantity
    {
        get
        {
            return NumCollections * CollectedQuantity;
        }
    }

    public enum ResourceType
    {
        Unspecified,
        Rock,
        ThinWood,
        MediumWood,
        ThickWood,
    }

    [field: SerializeField]
    public ResourceType Type
    {
        get;
        private set;
    }
}
