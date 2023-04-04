using UnityEngine;

public static class AudioSourceHelpers
{
    public class Section
    {
        public float Start;
        public float End;

        public float Duration
        {
            get => End - Start;
        }
    }

    public static void PlaySoundInterval(AudioSource audioSource, Section section, float pitch = 1.0f)
    {
        audioSource.Stop();
        audioSource.time = section.Start;
        audioSource.pitch = pitch;
        audioSource.Play();
        audioSource.SetScheduledEndTime(AudioSettings.dspTime + section.Duration);
    }
}
