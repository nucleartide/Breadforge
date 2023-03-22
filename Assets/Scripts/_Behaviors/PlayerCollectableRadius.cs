using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerCollectableRadius : MonoBehaviour
{
    // take the position of the transform
    // use Physics.CapsuleCast

    [SerializeField]
    [NotNull]
    GameObject collectibleSignifier;

    [SerializeField]
    Vector3 collectibleSignifierOffset = new Vector3(0f, 1f, 0f);

    HashSet<GameObject> nearbyCollectibleResources = new HashSet<GameObject>();

    void Update()
    {
        var position = transform.position;
        var p1 = position - transform.right * .1f;
        var p2 = position + transform.right * .1f;
        var radius = .25f;
        var castLength = 1f;

        // Cast a capsule by `castLength` meters forward to see if any colliders were hit.
        var hits = Physics.CapsuleCastAll(p1, p2, radius, transform.forward, castLength, LayerMask.GetMask(LayerHelpers.COLLECTIBLE_RESOURCE))
            .Where(hit => hit.distance > 0)
            .ToList();
        Debug.DrawRay(p1, transform.forward * castLength, Color.green);
        Debug.DrawRay(p2, transform.forward * castLength, Color.green);

        // Compute hit game objects.
        var previousFrame = nearbyCollectibleResources;
        var currentFrame = new HashSet<GameObject>(hits.Select(hit => hit.collider.gameObject));

        // Log the hits, and set appropriate layers for newly added and newly removed GameObjects.
        Debug.Log("Hits:");
        if (hits.Count == 0)
        {
            foreach (var obj in previousFrame)
                SetLayer(obj, LayerMask.NameToLayer(LayerHelpers.DEFAULT));
        }
        else
        {
            foreach (var hit in hits)
            {
                Debug.Log(hit.collider.gameObject.name);

                var newlyAdded = currentFrame.Except(previousFrame);
                foreach (var obj in newlyAdded)
                    SetLayer(obj, LayerMask.NameToLayer(LayerHelpers.COLLECTIBLE_RESOURCE_VISUAL));

                var newlyRemoved = previousFrame.Except(currentFrame);
                foreach (var obj in newlyRemoved)
                    SetLayer(obj, LayerMask.NameToLayer(LayerHelpers.DEFAULT));
            }
        }

        Debug.Log(previousFrame.Count + " " + currentFrame.Count);

        // Finally, update nearbyCollectibleResources so that we can perform the same check next frame.
        nearbyCollectibleResources = currentFrame;

        SetCollectibleSignifierPosition(hits, collectibleSignifier, collectibleSignifierOffset);
    }

    /// <summary>
    /// Given a list of RaycastHits, find the closest RaycastHit, and set the collectible signifier to the position of the closest RaycastHit.
    ///
    /// That was a mouthful. Basically, "show an E on the closest resource".
    /// </summary>
    private static void SetCollectibleSignifierPosition(List<RaycastHit> hits, GameObject collectibleSignifier, Vector3 collectibleSignifierOffset)
    {
        collectibleSignifier.SetActive(hits.Count > 0);
        if (hits.Count == 0)
            return;

        var minHit = ListHelpers.MinBy(hits, (a, b) => a.distance < b.distance);
        collectibleSignifier.transform.position = minHit.collider.transform.position + collectibleSignifierOffset;
    }

    private static void SetLayer(GameObject resourceGameObject, int layer)
    {
        resourceGameObject.GetComponent<ResourceVisual>().Visual.layer = layer;
    }

#if false
    void Update()
    {
        // Change the material of all hit colliders
        // to use a transparent Shader
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Renderer rend = hit.transform.GetComponent<Renderer>();

            if (rend)
            {
                rend.material.shader = Shader.Find("Transparent/Diffuse");
                Color tempColor = rend.material.color;
                tempColor.a = 0.3F;
                rend.material.color = tempColor;
            }
        }
    }
#endif
}
