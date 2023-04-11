using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField]
    [NotNull]
    private AudioSource footstepAudioSource;

    [SerializeField]
    [NotNull]
    private AudioSource footstepStoneAudioSource;

    [SerializeField]
    [NotNull]
    private PlayerAnimationEvents playerAnimationEvents;

    [SerializeField]
    [NotNull]
    private PlayerMovingState playerMovingState;

    [SerializeField]
    [NotNull]
    private PlayerStateMachine playerStateMachine;

    [SerializeField]
    [NotNull(IgnorePrefab = true)]
    private GameCamera gameCamera;

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

    private List<AudioSourceHelpers.Section> stoneFootsteps = new List<AudioSourceHelpers.Section>
    {
        new AudioSourceHelpers.Section { Start = .1f, End = .38f },
        new AudioSourceHelpers.Section { Start = .7f, End = .96f },
        new AudioSourceHelpers.Section { Start = 1.26f, End = 1.5f },
        new AudioSourceHelpers.Section { Start = 1.81f, End = 2.11f },
        new AudioSourceHelpers.Section { Start = 2.40f, End = 2.75f },
        new AudioSourceHelpers.Section { Start = 3.02f, End = 3.34f },
    };

    private enum GroundType
    {
        Gravel,
        Rock,
    }

    private void PlayWalkSound(GroundType groundType)
    {
        if (groundType == GroundType.Gravel)
        {
            var section = walkFootsteps[UnityEngine.Random.Range(0, walkFootsteps.Count)];
            AudioSourceHelpers.PlaySoundInterval(footstepAudioSource, section, gameCamera.OrthoSizeInverseLerp);
        }
        else if (groundType == GroundType.Rock)
        {
            var section = stoneFootsteps[UnityEngine.Random.Range(0, stoneFootsteps.Count)];
            AudioSourceHelpers.PlaySoundInterval(footstepStoneAudioSource, section, gameCamera.OrthoSizeInverseLerp);
        }
    }

    private void PlayRunSound(GroundType groundType)
    {
        if (groundType == GroundType.Gravel)
        {
            var section = runFootsteps[UnityEngine.Random.Range(0, runFootsteps.Count)];
            AudioSourceHelpers.PlaySoundInterval(footstepAudioSource, section, gameCamera.OrthoSizeInverseLerp);
        }
        else if (groundType == GroundType.Rock)
        {
            var section = stoneFootsteps[UnityEngine.Random.Range(0, stoneFootsteps.Count)];
            AudioSourceHelpers.PlaySoundInterval(footstepStoneAudioSource, section, gameCamera.OrthoSizeInverseLerp, 1.2f);
        }
    }

    private GroundType GroundUnderPlayer
    {
        get
        {
            return playerStateMachine.IsOverRock ? GroundType.Rock : GroundType.Gravel;
        }
    }

    private void PlayerAnimationEvents_OnWalkFootstep(object sender, EventArgs args)
    {
        if (playerMovingState.MovingState == PlayerMovingState.MovementState.Walking)
            PlayWalkSound(GroundUnderPlayer);
        else if (playerMovingState.MovingState == PlayerMovingState.MovementState.Running)
            PlayRunSound(GroundUnderPlayer);
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
