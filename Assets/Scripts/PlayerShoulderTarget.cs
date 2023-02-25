using UnityEngine;

public class PlayerShoulderTarget : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 180f;

    [SerializeField]
    float minXAngle = -20f;

    [SerializeField]
    float maxXAngle = 40f;

    void Update()
    {
        // Apply horizontal rotation.
        var horizontalInput = Input.GetAxis("Mouse X");
        transform.rotation *= Quaternion.AngleAxis(horizontalInput * rotationSpeed * Time.smoothDeltaTime, Vector3.up);

        // Apply vertical rotation.
        var verticalInput = Input.GetAxis("Mouse Y");
        transform.rotation *= Quaternion.AngleAxis(-1f * verticalInput * rotationSpeed * Time.smoothDeltaTime, Vector3.right);

        // Constrain rotations about the x and y axes.
        var x = MathHelpers.RotationClamp(transform.localEulerAngles.x, minXAngle, maxXAngle);
        transform.localEulerAngles = new Vector3(x, transform.localEulerAngles.y, 0f);
    }
}
