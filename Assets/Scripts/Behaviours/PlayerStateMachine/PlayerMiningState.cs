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
        if (resourceBeingCollected != null)
            resourceCollectTime += Time.deltaTime;
        else
            resourceCollectTime = 0f;
    }

    protected override void OnCollectCompleted()
    {
        Debug.Log("Mined thing.");
    }
}
