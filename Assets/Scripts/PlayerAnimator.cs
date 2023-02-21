using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private Animator animator;

    [SerializeField]
    [NotNull]
    private PlayerController playerController;

    private const string ANIMATOR_MOVEMENT_SPEED = "MovementSpeed";
    private int movementSpeedHash = -1;

    private void Start()
    {
        movementSpeedHash = Animator.StringToHash(ANIMATOR_MOVEMENT_SPEED);
    }

    private void Update()
    {
        // animator.SetInteger(movementSpeedHash, (int)playerController.MovementState);
    }
}
