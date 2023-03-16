using UnityEngine;

public class PlayerCollectableRadius : MonoBehaviour
{
    // take the position of the transform
    // use Physics.CapsuleCast

    void Awake()
    {
        Debug.Log(LayerHelpers.CollectibleResource);
    }

    void Update()
    {
        var position = transform.position;
        var p1 = position - transform.right;
        var p2 = position + transform.right;
        var radius = .5f;
        var castLength = 1f;

        // Cast a capsule by `castLength` meters forward to see if any colliders were hit.
        var hits = Physics.CapsuleCastAll(p1, p2, radius, transform.forward, castLength, LayerHelpers.CollectibleResource);
        Debug.DrawRay(p1, transform.forward * castLength, Color.green);
        Debug.DrawRay(p2, transform.forward * castLength, Color.green);

        // Log the hits.
        if (hits.Length > 0)
        {
            Debug.Log("Hits:");
            foreach (var hit in hits)
                Debug.Log(hit.collider.gameObject.name);
        }
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
