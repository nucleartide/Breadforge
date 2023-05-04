using UnityEngine;
using System;

public class PlayerMiningState : PlayerCollectingState
{
    [SerializeField]
    [NotNull]
    private PlayerAnimationEvents playerAnimationEvents;

    [SerializeField]
    [NotNull]
    private GameObject collectableCoal;

    [SerializeField]
    [NotNull]
    private GameObject collectableStone;

    [SerializeField]
    [NotNull]
    private GameObject collectableIronOre;

    [SerializeField]
    [NotNull]
    private GameObject collectableCopperOre;

    private void OnEnable()
    {
        playerAnimationEvents.OnPickaxeHit += PlayerAnimationEvents_OnPickaxeHit;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        playerAnimationEvents.OnPickaxeHit -= PlayerAnimationEvents_OnPickaxeHit;
    }

    private void PlayerAnimationEvents_OnPickaxeHit(object sender, EventArgs eventArgs)
    {
        Collect();
    }

    protected override void OnCollectCompleted()
    {
        // Old placeholder feedback.
        Debug.Log("Mined thing.");

        // TODO: spawn collectible resource at prefab's location.
        // ...

        // TODO: spawn should have some velocity to it.
        // ...
    }

    protected override float GetAmountCollectedPerAction()
    {
        return playerConfiguration.AmountMinedPerSwing;
    }
}
