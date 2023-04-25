using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NearbyResourcesManager : MonoBehaviour
{
    [NotNull(IgnorePrefab = true)]
    [SerializeField]
    private ImmediateCollectableManager immediateCollectable;

    [NotNull(IgnorePrefab = true)]
    [SerializeField]
    private PlayerStateMachine playerController;

    [NotNull]
    [SerializeField]
    private ResourceConfiguration waterResourceConfig;

    private RaycastHitGameObjects lastFrameCollectibles = new RaycastHitGameObjects();

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

    private class RaycastHitGameObjects
    {
        public HashSet<GameObject> WaterObjects = new HashSet<GameObject>();
        public HashSet<GameObject> NonWaterObjects = new HashSet<GameObject>();
    }

    private static RaycastHitGameObjects UpdateLayers(List<RaycastHit> hits, bool isCollecting, ResourceConfiguration waterResourceConfig, RaycastHitGameObjects previouslyHitObjects)
    {
        var previousFrame = previouslyHitObjects.NonWaterObjects;
        var currentFrame = new HashSet<GameObject>(
            hits
                .Where(hit =>
                {
                    var resource = hit.collider.GetComponent<Resource>();
                    return resource != null && resource.Configuration != waterResourceConfig;
                })
                .Select(hit => hit.collider.gameObject)
        );

        var previousFrameWaterTiles = previouslyHitObjects.WaterObjects;
        var currentFrameWaterTiles = new HashSet<GameObject>(
            hits
                .Where(hit =>
                {
                    var resource = hit.collider.GetComponent<Resource>();
                    return resource != null && resource.Configuration == waterResourceConfig;
                })
                .Select(hit => hit.collider.gameObject)
        );

        // Unset everything first.
        foreach (var obj in previousFrame)
            SetLayer(obj, LayerMask.NameToLayer(LayerHelpers.DEFAULT));

        // Then if the player isn't actively collecting a resource, highlight the resources hit by our raycast.
        if (!isCollecting)
            foreach (var obj in currentFrame)
                SetLayer(obj, LayerMask.NameToLayer(LayerHelpers.COLLECTIBLE_RESOURCE_VISUAL));

        // For the water tiles, unset everything first.
        foreach (var obj in previousFrameWaterTiles)
        {
            var visual = obj.GetComponent<ResourceVisual>().Visual;
            var highlighter = visual.GetComponent<WaterTileHighlighter>();
            highlighter.Unhighlight();
        }

        // For the water tiles, if the player isn't actively collecting a water tile, highlight the water tiles hit by our raycast.
        if (!isCollecting)
            foreach (var obj in currentFrameWaterTiles)
                obj.GetComponent<ResourceVisual>().Visual.GetComponent<WaterTileHighlighter>().Highlight();

        return new RaycastHitGameObjects
        {
            WaterObjects = currentFrameWaterTiles,
            NonWaterObjects = currentFrame,
        };
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
        lastFrameCollectibles = UpdateLayers(hits, isCollecting, waterResourceConfig, lastFrameCollectibles);
        immediateCollectable.SetImmediateCollectable(GetImmediateCollectable(hits));
    }
}
