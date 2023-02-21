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
        /// <summary>
        /// In units per second.
        /// </summary>
        public float PlayerSpeed;

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
        /// Whether player is jumping.
        /// </summary>
        public bool Jump;

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
        PlayerSpeed = 2.0f,
        JumpHeight = 1.0f,
        GravityValue = 9.81f,
        RotationSpeed = 3.0f,
    };

    [SerializeField]
    [NotNull]
    CharacterController characterController;

    float verticalVelocity;

    static CurrentInput GetCurrentInput(CharacterController controller)
    {
        var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        return new CurrentInput
        {
            Move = Vector3.ClampMagnitude(input, 1f),
            Jump = Input.GetButtonDown("Jump"),
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

    public enum PlayerMovementState
    {
        Idle = 0,
        Walking = 1,
        Running = 2,
    }

    public PlayerMovementState MovementState
    {
        get
        {
            var isWalking = GetCurrentInput(characterController).Move != Vector3.zero;
            var isRunning = Input.GetKey(KeyCode.LeftShift);

            if (isWalking && !isRunning)
                return PlayerMovementState.Walking;
            else if (isWalking && isRunning)
                return PlayerMovementState.Running;
            else
                return PlayerMovementState.Idle;
        }
    }

    void Update()
    {
        var currentInput = GetCurrentInput(characterController);
        verticalVelocity = UpdateVerticalVelocity(verticalVelocity, currentInput, configuration);
        if (currentInput.Move != Vector3.zero)
        {
            float singleStep = configuration.RotationSpeed * Time.smoothDeltaTime;
            // Vector3.Slerp is probably equivalent here.
            transform.forward = Vector3.RotateTowards(transform.forward, currentInput.Move.normalized, singleStep, 0f);
        }
        characterController.Move(currentInput.DeltaTime * configuration.PlayerSpeed * currentInput.Move);
        characterController.Move(currentInput.DeltaTime * new Vector3(0f, verticalVelocity, 0f));
    }
}
