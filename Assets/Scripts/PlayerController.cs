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

        public Vector3 Rotation;

        /// <summary>
        /// Whether player is jumping.
        /// </summary>
        public bool Jump;

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

    static CurrentInput GetCurrentInput(CharacterController controller)
    {
        // var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        var input = new Vector3(0f, 0f, Input.GetAxis("Vertical"));
        return new CurrentInput
        {
            Move = Vector3.ClampMagnitude(input, 1f),
            Rotation = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f),
            Jump = Input.GetButtonDown("Jump"),
            Run = Input.GetKey(KeyCode.LeftShift),
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

        if (currentInput.IsGrounded && currentInput.Jump)
        {
            // Then impart upward momentum.
            verticalVelocity += Mathf.Sqrt(config.JumpHeight * 3.0f * config.GravityValue);
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
        currentInput = GetCurrentInput(characterController);
        verticalSpeed = UpdateVerticalVelocity(verticalSpeed, currentInput, configuration);
        HorizontalSpeed =  Mathf.SmoothDamp(HorizontalSpeed, TargetSpeed, ref horizontalSpeedDampingValue, .3f);

        FaceMovementDirection();
        Move();
    }

    private void Move()
    {
        if (currentInput.Move.z > 0f)
            characterController.Move(currentInput.DeltaTime * HorizontalSpeed * transform.forward);
        characterController.Move(currentInput.DeltaTime * new Vector3(0f, verticalSpeed, 0f));
    }

    private void FaceMovementDirection()
    {

            transform.rotation *= Quaternion.AngleAxis(currentInput.Rotation.x * configuration.RotationSpeed * Time.smoothDeltaTime, Vector3.up);
        if (currentInput.Move != Vector3.zero)
        {
            float singleStep = configuration.RotationSpeed * Time.smoothDeltaTime;
            
            // transform.forward = Vector3.RotateTowards(transform.forward, transform.TransformDirection(currentInput.Move.normalized), singleStep, 0f);
        }
    }
}
