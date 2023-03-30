using UnityEngine;

public class PlayerShoulderTarget : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    PlayerShoulderConfiguration config;

    [SerializeField]
    [NotNull(IgnorePrefab = true)]
    GameInputManager gameInput;

    void Update()
    {
        var lookAround = gameInput.GetLookAround();

        // Apply horizontal rotation.
        var horizontalInput = lookAround.x;
        transform.rotation *= Quaternion.AngleAxis(horizontalInput * config.RotationSpeed * Time.smoothDeltaTime, Vector3.up);

        // Apply vertical rotation.
        var verticalInput = lookAround.y;
        transform.rotation *= Quaternion.AngleAxis(-1f * verticalInput * config.RotationSpeed * Time.smoothDeltaTime, Vector3.right);

        // Constrain rotations about the x and y axes.
        var x = MathHelpers.RotationClamp(transform.localEulerAngles.x, config.MinXAngle, config.MaxXAngle);
        transform.localEulerAngles = new Vector3(x, transform.localEulerAngles.y, 0f);
    }
}
