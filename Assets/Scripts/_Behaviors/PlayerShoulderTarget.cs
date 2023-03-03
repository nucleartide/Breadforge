using UnityEngine;

public class PlayerShoulderTarget : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 180f;

    [SerializeField]
    float minXAngle = -20f;

    [SerializeField]
    float maxXAngle = 40f;

    [SerializeField]
    [NotNull]
    InputManager gameInput;

    void Update()
    {
        var lookAround = gameInput.GetLookAround();

        // Apply horizontal rotation.
        var horizontalInput = lookAround.x;
        transform.rotation *= Quaternion.AngleAxis(horizontalInput * rotationSpeed * Time.smoothDeltaTime, Vector3.up);

        // Apply vertical rotation.
        var verticalInput = lookAround.y;
        transform.rotation *= Quaternion.AngleAxis(-1f * verticalInput * rotationSpeed * Time.smoothDeltaTime, Vector3.right);

        // Constrain rotations about the x and y axes.
        var x = MathHelpers.RotationClamp(transform.localEulerAngles.x, minXAngle, maxXAngle);
        transform.localEulerAngles = new Vector3(x, transform.localEulerAngles.y, 0f);
    }
}
