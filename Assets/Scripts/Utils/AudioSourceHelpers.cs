using UnityEngine;

public static class AudioSourceHelpers
{
    public static void PlaySoundInterval(AudioSource audioSource, float fromSeconds, float toSeconds)
    {
        audioSource.Stop();
        audioSource.time = fromSeconds;
        audioSource.Play();
        audioSource.SetScheduledEndTime(AudioSettings.dspTime + (toSeconds - fromSeconds));
    }
}
