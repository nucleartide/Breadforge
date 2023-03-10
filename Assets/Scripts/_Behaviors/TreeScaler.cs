using UnityEngine;

/// <summary>
/// Scales a Tree prefab by simultaneously scaling the Tree's BoxCollider and mesh visual with the same value.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class TreeScaler : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private new BoxCollider collider;

    [SerializeField]
    [NotNull]
    private Transform mesh;

    public void SetScale(float scale)
    {
        mesh.transform.localScale = new Vector3(scale, scale, scale);
        collider.size = new Vector3(scale, 1f, scale);
    }
}
