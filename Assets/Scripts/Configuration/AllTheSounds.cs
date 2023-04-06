using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class AllTheSounds : ScriptableObject
{
    public AudioClip PickaxeHit;

    public AudioClip BumpIntoCollider;

    public AudioClip NothingToMine;

    public AudioClip CollectWater;

    public AudioClip ChopMedium;

    [SerializeField]
    private List<AudioSourceHelpers.Section> chopMediumSections = new List<AudioSourceHelpers.Section>
    {
        new AudioSourceHelpers.Section { Start = 1.27f, End = 1.89f },
        new AudioSourceHelpers.Section { Start = 1.97f, End = 2.84f },
        new AudioSourceHelpers.Section { Start = 4.01f, End = 4.73f },
    };

    public AudioSourceHelpers.Section ChopMediumRandomSection
    {
        get => chopMediumSections[Random.Range(0, chopMediumSections.Count)];
    }

    public AudioClip ChopThicc;

    public AudioClip ChopThin;

    [SerializeField]
    private List<AudioSourceHelpers.Section> chopThinSections = new List<AudioSourceHelpers.Section>
    {
        new AudioSourceHelpers.Section { Start = .43f, End = .91f },
        new AudioSourceHelpers.Section { Start = 1.35f, End = 1.79f },
        new AudioSourceHelpers.Section { Start = 2.13f, End = 2.5f },
    };

    public AudioSourceHelpers.Section ChopThinRandomSection
    {
        get => chopThinSections[Random.Range(0, chopThinSections.Count)];
    }
}
