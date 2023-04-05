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

    public static void PlayClipAtPoint(AudioClip audioClip, Vector3 position, float volume = 1.0f, float pitch = 1.0f)
    {
        var gameObject = new GameObject(); // TODO(jason): There is no object pool here. Consider using the MMSoundManager from Feel framework.
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        gameObject.transform.position = position;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
        Object.Destroy(gameObject, audioClip.length);
    }
}
