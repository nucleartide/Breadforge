using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private Cinemachine.CinemachineVirtualCamera cinemachineCamera;

    [SerializeField]
    [NotNull(IgnorePrefab = true)]
    private Transform followedObject;

    [SerializeField]
    [Min(0f)]
    [NotNull(IgnorePrefab = true)]
    private GameInputManager gameInput;

    [SerializeField]
    private float cameraZoomScaleFactor = 5f;

    private void Awake()
    {
        cinemachineCamera.Follow = followedObject;
    }

    private void Update()
    {
        var zoomDelta = gameInput.GetZoom();
        if (zoomDelta != 0f)
            cinemachineCamera.m_Lens.OrthographicSize += zoomDelta * Time.deltaTime * -cameraZoomScaleFactor;

        // Prevent ortho size from becoming too small.
        cinemachineCamera.m_Lens.OrthographicSize = Mathf.Max(cinemachineCamera.m_Lens.OrthographicSize, 1f);
    }
}
