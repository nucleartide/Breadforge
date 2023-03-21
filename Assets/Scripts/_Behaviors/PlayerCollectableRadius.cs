using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerCollectableRadius : MonoBehaviour
{
    // take the position of the transform
    // use Physics.CapsuleCast

    HashSet<GameObject> nearbyCollectibleResources = new HashSet<GameObject>();

    void Update()
    {
        var position = transform.position;
        var p1 = position - transform.right;
        var p2 = position + transform.right;
        var radius = .5f;
        var castLength = 1f;

        // Cast a capsule by `castLength` meters forward to see if any colliders were hit.
        var hits = Physics.CapsuleCastAll(p1, p2, radius, transform.forward, castLength, LayerMask.GetMask(LayerHelpers.COLLECTIBLE_RESOURCE));
        Debug.DrawRay(p1, transform.forward * castLength, Color.green);
        Debug.DrawRay(p2, transform.forward * castLength, Color.green);

        // Compute hit game objects.
        var previousFrame = nearbyCollectibleResources;
        var currentFrame = new HashSet<GameObject>(hits.Select(hit => hit.collider.gameObject).ToList());

        // Log the hits, and set appropriate layers for newly added and newly removed GameObjects.
        Debug.Log("Hits:");
        if (hits.Length == 0)
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
