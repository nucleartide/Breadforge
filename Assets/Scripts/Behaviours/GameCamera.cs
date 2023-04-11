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
    private float zoomScaleFactor = 5f;

    [SerializeField]
    private float minOrthoSize = 1f;

    [SerializeField]
    private float maxOrthoSize = 10f;

    [SerializeField]
    private float initialOrthoSize = 3f;

    private void Awake()
    {
        cinemachineCamera.Follow = followedObject;
        cinemachineCamera.m_Lens.OrthographicSize = initialOrthoSize;
    }

    private void Update()
    {
        var zoomDelta = gameInput.GetZoom();
        if (zoomDelta != 0f)
            cinemachineCamera.m_Lens.OrthographicSize += zoomDelta * Time.deltaTime * -zoomScaleFactor;

        // Prevent ortho size from becoming too small.
        cinemachineCamera.m_Lens.OrthographicSize = Mathf.Clamp(cinemachineCamera.m_Lens.OrthographicSize, minOrthoSize, maxOrthoSize);
    }
}
