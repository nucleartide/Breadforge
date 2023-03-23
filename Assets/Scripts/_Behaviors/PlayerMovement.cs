using UnityEngine;

/// <summary>
/// PlayerController controls player movement.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    PlayerConfiguration playerConfiguration;

    [SerializeField]
    [NotNull(IgnorePrefab = true)]
    InputManager gameInput;

    [SerializeField]
    [NotNull]
    CharacterController characterController;

    public float HorizontalSpeed
    {
        get;
        private set;
    }

    /// <summary>
    /// Used to compute HorizontalSpeed. Do not use otherwise.
    /// </summary>
    float horizontalSpeedDampingValue;

    private float TargetSpeed
    {
        get
        {
            var isIdle = gameInput.GetMovement() == Vector3.zero;
            if (isIdle)
                return 0f;

            if (gameInput.GetRun())
                return playerConfiguration.PlayerRunSpeed;

            return playerConfiguration.PlayerWalkSpeed;
        }
    }

    private void Update()
    {
        HorizontalSpeed = Mathf.SmoothDamp(HorizontalSpeed, TargetSpeed, ref horizontalSpeedDampingValue, .3f);

        var movementDirection = gameInput.GetMovement();
        FaceMovementDirection(movementDirection);
        Move(movementDirection);
    }

    private void Move(Vector3 movementDirection)
    {
        var delta = Time.smoothDeltaTime * HorizontalSpeed * movementDirection;
        characterController.Move(delta);
    }

    private void FaceMovementDirection(Vector3 movementDirection)
    {
        if (gameInput.GetMovement() != Vector3.zero)
        {
            float singleStep = playerConfiguration.RotationSpeed * Time.smoothDeltaTime;
            transform.forward = Vector3.RotateTowards(transform.forward, movementDirection.normalized, singleStep, 0f);
        }
    }
}
