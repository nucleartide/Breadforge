using System;
using UnityEngine;

public class PlayerScoopingState : PlayerCollectingState
{
    [SerializeField]
    [NotNull]
    private PlayerAnimationEvents playerAnimationEvents;

    private void OnEnable()
    {
        playerAnimationEvents.OnPickUp += PlayerAnimationEvents_OnPickUp;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        playerAnimationEvents.OnPickUp -= PlayerAnimationEvents_OnPickUp;
    }

    private void PlayerAnimationEvents_OnPickUp(object sender, EventArgs eventArgs)
    {
        Collect();
    }

    protected override void OnCollectCompleted()
    {
        Debug.Log("Scooped thing.");
    }
}
