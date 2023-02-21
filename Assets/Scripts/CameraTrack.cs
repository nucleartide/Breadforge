using UnityEngine;

[ExecuteInEditMode]
public class CameraTrack : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    new Camera camera;

    [SerializeField]
    [NotNull]
    GameObject trackedObject;

    [SerializeField]
    Vector3 offset;

    void Update()
    {
        camera.transform.position = trackedObject.transform.position + offset;
    }
}
