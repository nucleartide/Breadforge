using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NearbyResourcesManager : MonoBehaviour
{
    [NotNull]
    [SerializeField]
    ImmediateCollectableManager immediateCollectable;

    [NotNull]
    [SerializeField]
    PlayerStateMachine playerController;

    HashSet<GameObject> lastFrameCollectibles = new HashSet<GameObject>();

    private static List<RaycastHit> Raycast(Transform transform)
    {
        var position = transform.position;
        var p1 = position - transform.right * .1f;
        var p2 = position + transform.right * .1f;
        var radius = .25f;
        var castLength = 1f;

#if UNITY_EDITOR
        Debug.DrawRay(p1, transform.forward * castLength, Color.green);
        Debug.DrawRay(p2, transform.forward * castLength, Color.green);
#endif

        // Cast a capsule by `castLength` meters forward to see if any colliders were hit.
        return Physics.CapsuleCastAll(p1, p2, radius, transform.forward, castLength, LayerMask.GetMask(LayerHelpers.COLLECTIBLE_RESOURCE))
            .Where(hit => hit.distance > 0)
            .ToList();
    }

    private static HashSet<GameObject> UpdateLayers(HashSet<GameObject> previouslyHitObjects, List<RaycastHit> hits, bool isCollecting)
    {
        var previousFrame = previouslyHitObjects;
        var currentFrame = new HashSet<GameObject>(hits.Select(hit => hit.collider.gameObject));

        // Unset everything first.
        foreach (var obj in previousFrame)
            SetLayer(obj, LayerMask.NameToLayer(LayerHelpers.DEFAULT));

        // Then if the player isn't actively collecting a resource, highlight the resources hit by our raycast.
        if (!isCollecting)
            foreach (var obj in currentFrame)
                SetLayer(obj, LayerMask.NameToLayer(LayerHelpers.COLLECTIBLE_RESOURCE_VISUAL));

        return currentFrame;
    }

    private static void SetLayer(GameObject resourceGameObject, int layer)
    {
        if (resourceGameObject != null)
            resourceGameObject.GetComponent<ResourceVisual>().Visual.layer = layer;
    }

    private static Resource GetImmediateCollectable(List<RaycastHit> hits)
    {
        var minHit = ListHelpers.MinBy(hits, (a, b) => a.distance < b.distance);
        if (minHit.collider == null)
            return null;

        return minHit.collider.GetComponent<Resource>();
    }

    private void Update()
    {
        var hits = Raycast(playerController.transform);
        var isCollecting = playerController.CurrentState is PlayerCollectingState;
        lastFrameCollectibles = UpdateLayers(lastFrameCollectibles, hits, isCollecting);
        Debug.Log($"Setting immediate collectable (hits: {hits.Count}):");
        Debug.Log(GetImmediateCollectable(hits));
        immediateCollectable.SetImmediateCollectable(GetImmediateCollectable(hits));
    }
}
