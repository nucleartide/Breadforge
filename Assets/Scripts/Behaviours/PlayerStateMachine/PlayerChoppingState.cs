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
    }

    private void OnDisable()
    {
        playerAnimationEvents.OnChopImpact -= PlayerAnimationEvents_OnChopImpact;
    }

    private void PlayerAnimationEvents_OnChopImpact(object sender, EventArgs eventArgs)
    {
        Collect();
    }

    protected override void OnCollectCompleted()
    {
        Debug.Log("Chopped thing.");
    }
}
