using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private AudioSource audioSource;

    [SerializeField]
    [NotNull]
    private PlayerAnimationEvents playerAnimationEvents;

    [SerializeField]
    [NotNull]
    private PlayerMovingState playerMovingState;

    private void OnEnable()
    {
        playerAnimationEvents.OnWalkFootstep += PlayerAnimationEvents_OnWalkFootstep;
    }

    private void OnDisable()
    {
        playerAnimationEvents.OnWalkFootstep -= PlayerAnimationEvents_OnWalkFootstep;
    }

    private void PlayerAnimationEvents_OnWalkFootstep(object sender, EventArgs args)
    {
        if (playerMovingState.MovingState == PlayerMovingState.MovementState.Walking)
            PlayWalkSound();
        else if (playerMovingState.MovingState == PlayerMovingState.MovementState.Running)
            PlayRunSound();
    }

    private class AudioClipSection
    {
        public float Start;
        public float End;
    }

    private List<AudioClipSection> walkFootsteps = new List<AudioClipSection>
    {
        new AudioClipSection { Start = 0f, End = .72f },
        new AudioClipSection { Start = 1.01f, End = 1.84f },
        new AudioClipSection { Start = 2.13f, End = 3.03f },
        new AudioClipSection { Start = 3.31f, End = 3.96f },
        new AudioClipSection { Start = 4.23f, End = 5.16f },
    };

    private List<AudioClipSection> runFootsteps = new List<AudioClipSection>
    {
        new AudioClipSection { Start = 6.34f, End = 7.07f },
        new AudioClipSection { Start = 7.34f, End = 8.11f },
        new AudioClipSection { Start = 8.39f, End = 9.10f },
        new AudioClipSection { Start = 9.40f, End = 10.11f },
        new AudioClipSection { Start = 10.36f, End = 11.14f },
    };

    private void PlayWalkSound()
    {
        var section = walkFootsteps[UnityEngine.Random.Range(0, walkFootsteps.Count)];
        AudioSourceHelpers.PlaySoundInterval(audioSource, section.Start, section.End);
    }

    private void PlayRunSound()
    {
        var section = runFootsteps[UnityEngine.Random.Range(0, runFootsteps.Count)];
        AudioSourceHelpers.PlaySoundInterval(audioSource, section.Start, section.End);
    }
}
