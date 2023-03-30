using UnityEngine;

/// <summary>
/// Track the position of a target GameObject, with an optional offset.
/// </summary>
public class PositionTracker : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private GameObject trackedObject;

    [SerializeField]
    private Vector3 offset = new Vector3(0f, 1.4f, 0f);

    private void Update()
    {
        transform.position = trackedObject.transform.position + offset;
    }
}
