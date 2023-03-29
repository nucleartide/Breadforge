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
    [NotNull]
    private StateEnum playerStateEnum;

    private void Awake()
    {
        movementSpeedHash = Animator.StringToHash(ANIMATOR_MOVEMENT_SPEED);
        playerStateHash = Animator.StringToHash(ANIMATOR_PLAYER_STATE);
    }

    private void OnEnable() => playerStateMachine.OnChanged += PlayerStateMachine_OnChanged;

    private void OnDisable() => playerStateMachine.OnChanged -= PlayerStateMachine_OnChanged;

    private void PlayerStateMachine_OnChanged(object sender, StateMachineBehaviour.StateMachineChangedArgs args)
    {
        var playerState = playerStateEnum.GetIndex(args.NewState);
        animator.SetInteger(playerStateHash, playerState);
    }

    private void Update()
    {
        animator.SetFloat(movementSpeedHash, playerMovingState.HorizontalSpeed);
    }
}
