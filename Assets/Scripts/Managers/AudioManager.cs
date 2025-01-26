using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;


    [SerializeField]
    private AudioSource source;

    private void Awake()
    {
        instance = this;
    }

    public void Play(AudioClip clip, float pitch)
    {
        source.pitch = pitch;
        source.PlayOneShot(clip);
    }
}
