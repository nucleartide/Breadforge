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

    private List<AudioSourceHelpers.Section> walkFootsteps = new List<AudioSourceHelpers.Section>
    {
        new AudioSourceHelpers.Section { Start = 0f, End = .72f },
        new AudioSourceHelpers.Section { Start = 1.01f, End = 1.84f },
        new AudioSourceHelpers.Section { Start = 2.13f, End = 3.03f },
        new AudioSourceHelpers.Section { Start = 3.31f, End = 3.96f },
        new AudioSourceHelpers.Section { Start = 4.23f, End = 5.16f },
    };

    private List<AudioSourceHelpers.Section> runFootsteps = new List<AudioSourceHelpers.Section>
    {
        new AudioSourceHelpers.Section { Start = 6.34f, End = 7.07f },
        new AudioSourceHelpers.Section { Start = 7.34f, End = 8.11f },
        new AudioSourceHelpers.Section { Start = 8.39f, End = 9.10f },
        new AudioSourceHelpers.Section { Start = 9.40f, End = 10.11f },
        new AudioSourceHelpers.Section { Start = 10.36f, End = 11.14f },
    };

    private void PlayWalkSound()
    {
        var section = walkFootsteps[UnityEngine.Random.Range(0, walkFootsteps.Count)];
        AudioSourceHelpers.PlaySoundInterval(audioSource, section);
    }

    private void PlayRunSound()
    {
        var section = runFootsteps[UnityEngine.Random.Range(0, runFootsteps.Count)];
        AudioSourceHelpers.PlaySoundInterval(audioSource, section);
    }

    private void PlayerAnimationEvents_OnWalkFootstep(object sender, EventArgs args)
    {
        if (playerMovingState.MovingState == PlayerMovingState.MovementState.Walking)
            PlayWalkSound();
        else if (playerMovingState.MovingState == PlayerMovingState.MovementState.Running)
            PlayRunSound();
    }

    private void OnEnable()
    {
        playerAnimationEvents.OnWalkFootstep += PlayerAnimationEvents_OnWalkFootstep;
    }

    private void OnDisable()
    {
        playerAnimationEvents.OnWalkFootstep -= PlayerAnimationEvents_OnWalkFootstep;
    }
}
