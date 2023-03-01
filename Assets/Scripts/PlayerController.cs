using System;
using UnityEngine;

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
    Transform playerShoulderTarget;

    [SerializeField]
    [NotNull]
    GameInput gameInput;

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
                return configuration.PlayerRunSpeed;

            return configuration.PlayerWalkSpeed;
        }
    }

    private void Update()
    {
        HorizontalSpeed = Mathf.SmoothDamp(HorizontalSpeed, TargetSpeed, ref horizontalSpeedDampingValue, .3f);

        var yRotation = Quaternion.Euler(0f, playerShoulderTarget.eulerAngles.y, 0f);
        var movementDirection = yRotation * gameInput.GetMovement();
        FaceMovementDirection(movementDirection);
        Move(movementDirection);
    }

    private void Move(Vector3 movementDirection)
    {
        transform.position += Time.smoothDeltaTime * HorizontalSpeed * movementDirection;
    }

    private void FaceMovementDirection(Vector3 movementDirection)
    {
        if (gameInput.GetMovement() != Vector3.zero)
        {
            float singleStep = configuration.RotationSpeed * Time.smoothDeltaTime;
            transform.forward = Vector3.RotateTowards(transform.forward, movementDirection.normalized, singleStep, 0f);
        }
    }
}
