using UnityEngine;

public class RotatingCamera : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private new Camera camera;

    [SerializeField]
    private Vector3 pivotPoint = Vector3.zero;

    [SerializeField]
    [NotNull]
    private Transform playerTransform;

    [SerializeField]
    private float radius = 50f;

    [SerializeField]
    private float yOffset = 10f;

    [SerializeField]
    private float timeScale = 2f;

    private void Update()
    {
        // Compute circular position.
        var circularPosition = new Vector3(
            playerTransform.position.x + radius * Mathf.Cos(Time.time * timeScale),
            playerTransform.position.y + yOffset,
            playerTransform.position.z + radius * Mathf.Sin(Time.time * timeScale)
        );

        // Update camera position.
        camera.transform.position = circularPosition;

        // Also look at center.
        camera.transform.LookAt(playerTransform.position);
    }
}
