using UnityEngine;
using System;

public class PlayerMiningState : PlayerCollectingState
{
    [SerializeField]
    [NotNull]
    private PlayerAnimationEvents playerAnimationEvents;

    private float resourceCollectTime = 0f;

    private void OnEnable()
    {
        playerAnimationEvents.OnPickaxeHit += PlayerAnimationEvents_OnPickaxeHit;
    }

    private void OnDisable()
    {
        playerAnimationEvents.OnPickaxeHit -= PlayerAnimationEvents_OnPickaxeHit;
    }

    private void PlayerAnimationEvents_OnPickaxeHit(object sender, EventArgs eventArgs)
    {
        if (resourceBeingCollected != null)
        {
            resourceBeingCollected.Elapse(resourceCollectTime);
            resourceCollectTime = 0f;
        }
    }

    protected override void UpdateResourceCollection()
    {
        throw new System.Exception("update debug log feedback.");
        throw new System.Exception("fill in UpdateResourceCollection implementation for other player states.");

        if (resourceBeingCollected != null)
            resourceCollectTime += Time.deltaTime;
        else
            resourceCollectTime = 0f;
    }
}
