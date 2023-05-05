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

        // Spawn collectible resource at prefab's location.
        var position = resourceBeingCollected.transform.position;
        var newCollectable = Instantiate(collectableCopperOre, position, Quaternion.identity);

        // Add some random impulse to the Collectable Resource.
        newCollectable.GetComponent<CollectableResource>().SetRandomInitialImpulse();
    }

    protected override float GetAmountCollectedPerAction()
    {
        return playerConfiguration.AmountMinedPerSwing;
    }
}
