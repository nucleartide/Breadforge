using UnityEngine;

/// <summary>
/// Unity's Tilemap system will apply a negative scale to flip objects, which
/// causes child Box Colliders to complain about negative scale values.
///
/// To fix this, add this script to any child GameObjects that contain BoxColliders.
///
/// This will detect if any parent object has a negative scale value, and
/// then apply the corresponding flip as a rotation on the Y-axis instead.
/// </summary>
public class FixFlippedTile : MonoBehaviour
{
    private void FixNegativeScale()
    {
        var xScale = 1f;
        var yScale = 1f;
        var zScale = 1f;

        var parent = transform.parent;
        while (parent != null)
        {
            // Compute the sign of the scale on each axis.
            xScale *= parent.localScale.x;
            yScale *= parent.localScale.y;
            zScale *= parent.localScale.z;

            // Tee up the next parent for processing.
            parent = parent.transform.parent;
        }

#if UNITY_EDITOR
        var flippedAxes = 0f;
        if (xScale != 1) flippedAxes++;
        if (yScale != 1) flippedAxes++;
        if (zScale != 1) flippedAxes++;
        if (flippedAxes > 1)
            throw new System.Exception("Hmm, there is more than one flipped axis. This script was designed to handle just one!");
#endif

        if (xScale != 1)
        {
            transform.rotation *= Quaternion.AngleAxis(180, Vector3.up);
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }
        else if (yScale != 1)
        {
            transform.rotation *= Quaternion.AngleAxis(180, Vector3.right);
            transform.localScale = new Vector3(transform.localScale.x, -1f, transform.localScale.z);
        }
        else if (zScale != 1)
        {
            transform.rotation *= Quaternion.AngleAxis(180, Vector3.up);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -1f);
        }
    }

    private void Start()
    {
        FixNegativeScale();
    }
}
