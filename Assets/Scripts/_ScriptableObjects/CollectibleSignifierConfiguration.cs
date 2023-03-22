using UnityEngine;

[CreateAssetMenu]
public class CollectibleSignifierConfiguration : ScriptableObject
{
    /// <summary>
    /// The "E" text that should be shown above the immediately-collectible resource.
    /// </summary>
    [NotNull]
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private Vector3 offset;

    private GameObject instantiatedPrefab;

    private void OnDisable() => instantiatedPrefab = null;

    /// <summary>
    /// Set the position of the collectible signifier.
    /// </summary>
    public void SetPosition(Vector3 position)
    {
        if (instantiatedPrefab == null)
            instantiatedPrefab = Instantiate(prefab);

        instantiatedPrefab.transform.position = position + offset;
    }

    /// <summary>
    /// Enable or disable the collectible signifier.
    /// </summary>
    public void SetActive(bool active)
    {
        if (instantiatedPrefab == null)
            instantiatedPrefab = Instantiate(prefab);

        instantiatedPrefab.SetActive(active);
    }
}
