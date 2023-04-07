using System;
using UnityEngine;

public class PlayerChoppingState : PlayerCollectingState
{
    [SerializeField]
    [NotNull]
    private PlayerAnimationEvents playerAnimationEvents;

    private void OnEnable()
    {
        playerAnimationEvents.OnChopImpact += PlayerAnimationEvents_OnChopImpact;
        playerAnimationEvents.OnChopImpactComplete += PlayerAnimationEvents_OnChopImpactComplete;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        playerAnimationEvents.OnChopImpact -= PlayerAnimationEvents_OnChopImpact;
        playerAnimationEvents.OnChopImpactComplete -= PlayerAnimationEvents_OnChopImpactComplete;
    }

    private void PlayerAnimationEvents_OnChopImpact(object sender, EventArgs eventArgs)
    {
        if (resourceBeingCollected.Type == ResourceConfiguration.ResourceType.ThinWood) 
            Collect();
    }

    private void PlayerAnimationEvents_OnChopImpactComplete(object sender, EventArgs eventArgs)
    {
        if (resourceBeingCollected.Type != ResourceConfiguration.ResourceType.ThinWood)
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
