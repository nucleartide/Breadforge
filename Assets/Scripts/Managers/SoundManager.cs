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
    }

    private void OnDisable()
    {
        playerAnimationEvents.OnPickaxeHit -= PlayerAnimationEvents_OnPickaxeHit;
    }

    private void PlayerAnimationEvents_OnPickaxeHit(object sender, EventArgs eventArgs)
    {
        AudioSource.PlayClipAtPoint(allTheSounds.PickaxeHit, player.position, 1.0f);
    }

    private void PlayerStateMachine_OnResourceCollisionEnter(object sender, EventArgs eventArgs)
    {
        AudioSourceHelpers.PlayClipAtPoint(allTheSounds.BumpIntoCollider, player.position, .7f, 1.2f);
    }

    private void Update()
    {
        throw new System.Exception("TODO(jason): Add collider to edges of world so you hear the bump sound there too");

#if false
        // TODO: This will be easier with the Sound MMFeedback, which has a test button that you can use without writing more code.
        if (Input.GetKeyDown(KeyCode.W))
            PlayBumpIntoColliderSound();
#endif
    }
}
