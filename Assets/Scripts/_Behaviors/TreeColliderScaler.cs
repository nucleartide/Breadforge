using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simultaneously scales a tree prefab's BoxCollider and mesh visual using the same scale value.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class TreeColliderScaler : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private new BoxCollider collider;

    [SerializeField]
    [NotNull]
    private Transform treeMesh;

    public void SetScale(float scale)
    {
        treeMesh.transform.localScale = new Vector3(scale, scale, scale);
        collider.size = new Vector3(scale, 1f, scale);
    }
}
