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
        playerStateMachine.OnResourceCollisionEnter += PlayerStateMachine_OnResourceCollisionEnter;
        playerStateMachine.OnNothingToMine += PlayerStateMachine_OnNothingToMine;
    }

    private void OnDisable()
    {
        playerAnimationEvents.OnPickaxeHit -= PlayerAnimationEvents_OnPickaxeHit;
        playerStateMachine.OnResourceCollisionEnter -= PlayerStateMachine_OnResourceCollisionEnter;
        playerStateMachine.OnNothingToMine -= PlayerStateMachine_OnNothingToMine;
    }

    private void PlayerAnimationEvents_OnPickaxeHit(object sender, EventArgs eventArgs)
    {
        AudioSourceHelpers.PlayClipAtPoint(allTheSounds.PickaxeHit, player.position, 1f);
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
            AudioSourceHelpers.PlayClipAtPoint(allTheSounds.NothingToMine, player.position, 1f);
    }
#endif
}
