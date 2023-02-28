using System;
using UnityEngine;

/// <summary>
/// Enables a GameObject to move with WASD and jump with Space.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Serializable]
    struct Configuration
    {
        public float PlayerWalkSpeed;
        public float PlayerRunSpeed;

        /// <summary>
        /// Scaling factor for jump height.
        /// </summary>
        public float JumpHeight;

        /// <summary>
        /// In units per second squared.
        /// </summary>
        public float GravityValue;

        /// <summary>
        /// In radians per second.
        /// </summary>
        public float RotationSpeed;
    }

    [Serializable]
    struct CurrentInput
    {
        /// <summary>
        /// Current movement direction.
        /// </summary>
        public Vector3 Move;

        /// <summary>
        /// Whether player is running.
        /// </summary>
        public bool Run;

        /// <summary>
        /// Time elapsed since last frame.
        /// </summary>
        public float DeltaTime;

        /// <summary>
        /// Whether player is grounded.
        /// </summary>
        public bool IsGrounded;
    }

    [SerializeField]
    Configuration configuration = new()
    {
        PlayerWalkSpeed = 2.0f,
        PlayerRunSpeed = 4.0f,
        JumpHeight = 1.0f,
        GravityValue = 9.81f,
        RotationSpeed = 3.0f,
    };

    [SerializeField]
    [NotNull]
    CharacterController characterController;

    [SerializeField]
    [NotNull]
    Transform playerShoulderTarget;

    [SerializeField]
    [NotNull]
    GameInput gameInput;

    CurrentInput currentInput;
    float verticalSpeed;

    public float HorizontalSpeed
    {
        get;
        private set;
    }

    /// <summary>
    /// Used to compute currentSpeed. Do not use otherwise.
    /// </summary>
    float horizontalSpeedDampingValue;

    static CurrentInput GetCurrentInput(CharacterController controller, GameInput gameInput)
    {
        var movement = gameInput.GetMovementNormalized();
        var input = new Vector3(movement.x, 0f, movement.y);
        return new CurrentInput
        {
            Move = Vector3.ClampMagnitude(input, 1f),
            Run = gameInput.GetRun(),
            DeltaTime = Time.smoothDeltaTime,
            IsGrounded = controller.isGrounded,
        };
    }

    static float UpdateVerticalVelocity(float verticalVelocity, CurrentInput currentInput, Configuration config)
    {
        // Simulate force of gravity.
        verticalVelocity -= config.GravityValue * Time.deltaTime;

        // However,
        if (currentInput.IsGrounded && verticalVelocity < 0)
        {
            // Then zero out y-component of velocity.
            // Must be slightly negative so that CharacterController's .isGrounded computation returns true.
            verticalVelocity = -.5f;
        }

        return verticalVelocity;
    }

    private float TargetSpeed
    {
        get
        {
            var isIdle = currentInput.Move == Vector3.zero;
            if (isIdle)
                return 0f;

            if (currentInput.Run)
                return configuration.PlayerRunSpeed;

            return configuration.PlayerWalkSpeed;
        }
    }

    private void Update()
    {
        currentInput = GetCurrentInput(characterController, gameInput);
        verticalSpeed = UpdateVerticalVelocity(verticalSpeed, currentInput, configuration);
        HorizontalSpeed = Mathf.SmoothDamp(HorizontalSpeed, TargetSpeed, ref horizontalSpeedDampingValue, .3f);

        var yRotation = Quaternion.Euler(0f, playerShoulderTarget.eulerAngles.y, 0f);
        var movementDirection = yRotation * currentInput.Move;
        FaceMovementDirection(movementDirection);
        Move(movementDirection);
    }

    private void Move(Vector3 movementDirection)
    {
        characterController.Move(currentInput.DeltaTime * HorizontalSpeed * movementDirection);
        characterController.Move(currentInput.DeltaTime * new Vector3(0f, verticalSpeed, 0f));
    }

    private void FaceMovementDirection(Vector3 movementDirection)
    {
        if (currentInput.Move != Vector3.zero)
        {
            float singleStep = configuration.RotationSpeed * Time.smoothDeltaTime;
            transform.forward = Vector3.RotateTowards(transform.forward, movementDirection.normalized, singleStep, 0f);
        }
    }
}
