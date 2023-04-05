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
        AudioSource.PlayClipAtPoint(allTheSounds.PickaxeHit, player.position, 1.0f);
    }

    private void PlayBumpIntoColliderSound()
    {
        AudioSource.PlayClipAtPoint(allTheSounds.BumpIntoCollider, player.position, 1.0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            PlayBumpIntoColliderSound();
    }
}
