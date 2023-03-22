using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private Cinemachine.CinemachineVirtualCamera cinemachineCamera;

    [SerializeField]
    [NotNull(IgnorePrefab = true)]
    private Transform followedObject;

    private void Awake()
    {
        cinemachineCamera.Follow = followedObject;
    }
}
