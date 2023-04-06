using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    [NotNull(IgnorePrefab = true)]
    private PlayerAnimationEvents playerAnimationEvents;

    [SerializeField]
    [NotNull]
    private AllTheSounds allTheSounds;

    [SerializeField]
    [NotNull(IgnorePrefab = true)]
    [Tooltip("Needed so we can play sounds at the right position in 3D space.")]
    private Transform player;

    [SerializeField]
    [NotNull(IgnorePrefab = true)]
    private PlayerStateMachine playerStateMachine;

    private void OnEnable()
    {
        playerAnimationEvents.OnPickaxeHit += PlayerAnimationEvents_OnPickaxeHit;
        playerAnimationEvents.OnPickUp += PlayerAnimationEvents_OnPickUp;
        playerAnimationEvents.OnChopImpact += PlayerAnimationEvents_OnChopImpact;
        playerStateMachine.OnResourceCollisionEnter += PlayerStateMachine_OnResourceCollisionEnter;
        playerStateMachine.OnNothingToMine += PlayerStateMachine_OnNothingToMine;
    }

    private void OnDisable()
    {
        playerAnimationEvents.OnPickaxeHit -= PlayerAnimationEvents_OnPickaxeHit;
        playerAnimationEvents.OnPickUp -= PlayerAnimationEvents_OnPickUp;
        playerAnimationEvents.OnChopImpact -= PlayerAnimationEvents_OnChopImpact;
        playerStateMachine.OnResourceCollisionEnter -= PlayerStateMachine_OnResourceCollisionEnter;
        playerStateMachine.OnNothingToMine -= PlayerStateMachine_OnNothingToMine;
    }

    private void PlayerAnimationEvents_OnPickaxeHit(object sender, EventArgs eventArgs)
    {
        AudioSourceHelpers.PlayClipAtPoint(allTheSounds.PickaxeHit, player.position, 1f);
    }

    private void PlayerAnimationEvents_OnPickUp(object sender, EventArgs eventArgs)
    {
        AudioSourceHelpers.PlayClipAtPoint(allTheSounds.CollectWater, player.position, .25f, .7f);
    }

    private void PlayerAnimationEvents_OnChopImpact(object sender, EventArgs eventArgs)
    {
        var currentState = playerStateMachine.CurrentState;
        if (currentState is PlayerChoppingState choppingState)
        {
            var resourceType = choppingState.ResourceType;
            switch (resourceType)
            {
                case ResourceConfiguration.ResourceType.MediumWood:
                    AudioSourceHelpers.PlayIntervalAtPoint(allTheSounds.ChopMedium, allTheSounds.ChopMediumRandomSection, player.position);
                    break;
                case ResourceConfiguration.ResourceType.ThinWood:
                    AudioSourceHelpers.PlayIntervalAtPoint(allTheSounds.ChopThin, allTheSounds.ChopThinRandomSection, player.position);
                    break;
                case ResourceConfiguration.ResourceType.ThickWood:
                    AudioSourceHelpers.PlayClipAtPoint(allTheSounds.ChopThicc, player.position, 1f, UnityEngine.Random.Range(.8f, 1.2f));
                    break;
                default:
                    throw new System.Exception($"Resource type {resourceType} is unsupported.");
            };
        }
    }

    private void PlayerStateMachine_OnResourceCollisionEnter(object sender, EventArgs eventArgs)
    {
        AudioSourceHelpers.PlayClipAtPoint(allTheSounds.BumpIntoCollider, player.position, .7f, 1.2f);
    }

    private void PlayerStateMachine_OnNothingToMine(object sender, EventArgs eventArgs)
    {
        AudioSourceHelpers.PlayClipAtPoint(allTheSounds.NothingToMine, player.position, 1f);
    }

#if false
    private void Update()
    {
        // TODO(jason): Testing sounds is easier with the Feel framework, which has a "test sound" button for Sound feedbacks.
        if (Input.GetKeyDown(KeyCode.Q))
            AudioSourceHelpers.PlayIntervalAtPoint(allTheSounds.ChopMedium, allTheSounds.ChopMediumRandomSection, player.position);
        else if (Input.GetKeyDown(KeyCode.R))
            AudioSourceHelpers.PlayClipAtPoint(allTheSounds.ChopThicc, player.position, 1f, UnityEngine.Random.Range(.8f, 1.2f));
        else if (Input.GetKeyDown(KeyCode.T))
            AudioSourceHelpers.PlayIntervalAtPoint(allTheSounds.ChopThin, allTheSounds.ChopThinRandomSection, player.position);
    }
#endif
}
