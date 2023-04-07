using System;
using UnityEngine;

public class PlayerChoppingState : PlayerCollectingState
{
    [SerializeField]
    [NotNull]
    private PlayerAnimationEvents playerAnimationEvents;

    private void OnEnable()
    {
        playerAnimationEvents.OnChopImpactComplete += PlayerAnimationEvents_OnChopImpact;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        playerAnimationEvents.OnChopImpactComplete -= PlayerAnimationEvents_OnChopImpact;
    }

    private void PlayerAnimationEvents_OnChopImpact(object sender, EventArgs eventArgs)
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
