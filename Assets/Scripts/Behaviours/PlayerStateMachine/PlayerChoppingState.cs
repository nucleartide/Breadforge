using System;
using UnityEngine;

public class PlayerChoppingState : PlayerCollectingState
{
    [SerializeField]
    [NotNull]
    private PlayerAnimationEvents playerAnimationEvents;

    private void OnEnable()
    {
        playerAnimationEvents.OnLateChopImpact += PlayerAnimationEvents_OnLateChopImpact;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        playerAnimationEvents.OnLateChopImpact -= PlayerAnimationEvents_OnLateChopImpact;
    }

    private void PlayerAnimationEvents_OnLateChopImpact(object sender, EventArgs eventArgs)
    {
        Collect();
    }

    protected override void OnCollectCompleted()
    {
        Debug.Log("Chopped thing.");
    }

    protected override float GetAmountCollectedPerAction()
    {
        return playerConfiguration.AmountChoppedPerSwing;
    }
}
