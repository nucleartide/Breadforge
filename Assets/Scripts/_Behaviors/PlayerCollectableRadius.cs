using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerCollectableRadius : MonoBehaviour
{
    [NotNull]
    [SerializeField]
    CollectibleSignifierManager canCollectSignifier;

    /// <summary>
    /// A Resource that can be actively collected by the player.
    ///
    /// If the player is moved out of range, this will return null.
    /// </summary>
    public Resource CanCollectResource
    {
        get;
        private set;
    }

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

    private static HashSet<GameObject> UpdateLayers(HashSet<GameObject> previouslyHitObjects, List<RaycastHit> hits)
    {
        var previousFrame = previouslyHitObjects;
        var currentFrame = new HashSet<GameObject>(hits.Select(hit => hit.collider.gameObject));

        // Set appropriate layers for newly added and newly removed GameObjects.
        if (hits.Count == 0)
        {
            foreach (var obj in previousFrame)
                SetLayer(obj, LayerMask.NameToLayer(LayerHelpers.DEFAULT));
        }
        else
        {
            foreach (var hit in hits)
            {
                var newlyAdded = currentFrame.Except(previousFrame);
                foreach (var obj in newlyAdded)
                    SetLayer(obj, LayerMask.NameToLayer(LayerHelpers.COLLECTIBLE_RESOURCE_VISUAL));

                var newlyRemoved = previousFrame.Except(currentFrame);
                foreach (var obj in newlyRemoved)
                    SetLayer(obj, LayerMask.NameToLayer(LayerHelpers.DEFAULT));
            }
        }

        return currentFrame;
    }

    /// <summary>
    /// Given a list of RaycastHits, find the closest RaycastHit, and set the collectible signifier to the position of the closest RaycastHit.
    ///
    /// That was a mouthful. Basically, "show an E on the closest resource".
    /// </summary>
    private static void SetCollectibleSignifierPosition(List<RaycastHit> hits, CollectibleSignifierManager signifier, Resource canCollectResource)
    {
        signifier.SetActive(canCollectResource != null);
        if (canCollectResource == null)
            return;
        else
            signifier.SetPosition(canCollectResource.transform.position);
    }

    private static void SetLayer(GameObject resourceGameObject, int layer)
    {
        if (resourceGameObject != null)
            resourceGameObject.GetComponent<ResourceVisual>().Visual.layer = layer;
    }

    private static Resource UpdateCanCollectResource(List<RaycastHit> hits)
    {
        var minHit = ListHelpers.MinBy(hits, (a, b) => a.distance < b.distance);
        if (minHit.collider == null)
            return null;

        return minHit.collider.GetComponent<Resource>();
    }

    private void Update()
    {
        var hits = Raycast(transform);
        lastFrameCollectibles = UpdateLayers(lastFrameCollectibles, hits);
        CanCollectResource = UpdateCanCollectResource(hits);
        SetCollectibleSignifierPosition(hits, canCollectSignifier, CanCollectResource);
    }
}
