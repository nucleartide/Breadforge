using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private Animator animator;

    [SerializeField]
    [NotNull]
    private PlayerStateMachine playerStateMachine;

    [SerializeField]
    [NotNull]
    private PlayerMovingState playerMovingState;

    private const string ANIMATOR_MOVEMENT_SPEED = "MovementSpeed";

    private const string ANIMATOR_PLAYER_STATE = "PlayerState";

    private int movementSpeedHash = -1;

    private int playerStateHash = -1;

    [SerializeField]
    List<AnimationEnum> playerStateEnums;

    private void Start()
    {
        movementSpeedHash = Animator.StringToHash(ANIMATOR_MOVEMENT_SPEED);
        playerStateHash = Animator.StringToHash(ANIMATOR_PLAYER_STATE);
    }

    private int GetPlayerStateIndex()
    {
        var state = playerStateMachine.CurrentState;
        foreach (var animEnum in playerStateEnums)
            if (state.GetType().Name == animEnum.State.name)
                return animEnum.Index;

        throw new System.Exception($"Current state {state.GetType().Name} does not have a PlayerState animation index. Please configure a new AnimationEnum, and add it to the PlayerAnimator.");
    }

    private void Update()
    {
        animator.SetFloat(movementSpeedHash, playerMovingState.HorizontalSpeed);
        animator.SetInteger(playerStateHash, GetPlayerStateIndex());
    }
}
