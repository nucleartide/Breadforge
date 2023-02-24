using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private GameObject trackedObject;

    [SerializeField]
    private Vector3 offset = new Vector3(0f, 1.4f, 0f);

    void Update()
    {
        transform.position = trackedObject.transform.position + offset;
    }
}
