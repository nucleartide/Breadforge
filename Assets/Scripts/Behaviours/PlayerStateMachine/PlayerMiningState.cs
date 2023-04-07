using UnityEngine;
using System;

public class PlayerMiningState : PlayerCollectingState
{
    [SerializeField]
    [NotNull]
    private PlayerAnimationEvents playerAnimationEvents;

    private void OnEnable()
    {
        playerAnimationEvents.OnPickaxeHitComplete += PlayerAnimationEvents_OnPickaxeHit;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        playerAnimationEvents.OnPickaxeHitComplete -= PlayerAnimationEvents_OnPickaxeHit;
    }

    private void PlayerAnimationEvents_OnPickaxeHit(object sender, EventArgs eventArgs)
    {
        Collect();
    }

    protected override void OnCollectCompleted()
    {
        Debug.Log("Mined thing.");
    }

    protected override float GetAmountCollectedPerAction()
    {
        return playerConfiguration.AmountMinedPerSwing;
    }
}
