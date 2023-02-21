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

    private const string MOVEMENT_STATE = "MovementState";
    private int movementStateHash = -1;

    private void Start()
    {
        movementStateHash = Animator.StringToHash(MOVEMENT_STATE);
    }

    private void Update()
    {
        animator.SetInteger(movementStateHash, (int)playerController.MovementState);
    }
}
