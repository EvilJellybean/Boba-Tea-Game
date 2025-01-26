using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;


    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioSource playOneAtATime;
    [SerializeField]
    private float playOneAtATimeInterval = 3;

    private float lastPlayed = -100;

    private void Awake()
    {
        instance = this;
    }

    public void Play(AudioClip clip, float pitch)
    {
        source.pitch = pitch;
        source.PlayOneShot(clip);
    }

    public void PlayOneAtATime(AudioClip clip, float pitch)
    {
        if(playOneAtATime.isPlaying || Time.time - lastPlayed < playOneAtATimeInterval)
        {
            return;
        }
        lastPlayed = Time.time;

        playOneAtATime.pitch = pitch;
        playOneAtATime.PlayOneShot(clip);
    }
}
