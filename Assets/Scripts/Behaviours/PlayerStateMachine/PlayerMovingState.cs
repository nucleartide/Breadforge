using UnityEngine;

public class PlayerMovingState : StateBehaviour
{
    [SerializeField]
    [NotNull]
    PlayerConfiguration playerConfiguration;

    [SerializeField]
    [NotNull(IgnorePrefab = true)]
    GameInputManager gameInput;

    [SerializeField]
    [NotNull]
    CharacterController characterController;

    /// <summary>
    /// Used to compute HorizontalSpeed. Do not use otherwise.
    /// </summary>
    float horizontalSpeedDampingValue;

    public float HorizontalSpeed
    {
        get;
        private set;
    }

    public enum MovementState
    {
        Idle,
        Walking,
        Running,
    }

    public MovementState MovingState
    {
        get
        {
            var isIdle = gameInput.GetMovement() == Vector3.zero;
            if (isIdle)
                return MovementState.Idle;

            if (gameInput.GetRun())
                return MovementState.Running;

            return MovementState.Walking;
        }
    }

    private float TargetSpeed
    {
        get
        {
            return MovingState switch
            {
                MovementState.Idle => 0f,
                MovementState.Walking => playerConfiguration.PlayerWalkSpeed,
                MovementState.Running => playerConfiguration.PlayerRunSpeed,
                _ => throw new System.Exception($"MovementState {MovingState} is not handled.")
            };
        }
    }

    private void Update()
    {
        HorizontalSpeed = Mathf.SmoothDamp(HorizontalSpeed, TargetSpeed, ref horizontalSpeedDampingValue, .15f);

        var movementDirection = gameInput.GetMovement();
        FaceMovementDirection(movementDirection);
        Move(movementDirection);
    }

    private void Move(Vector3 movementDirection)
    {
        var delta = Time.deltaTime * HorizontalSpeed * movementDirection;
        characterController.Move(delta);
    }

    private void FaceMovementDirection(Vector3 movementDirection)
    {
        if (gameInput.GetMovement() != Vector3.zero)
        {
            float singleStep = playerConfiguration.RotationSpeed * Time.deltaTime;
            transform.forward = Vector3.RotateTowards(transform.forward, movementDirection.normalized, singleStep, 0f);
        }
    }
}
