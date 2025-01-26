
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSfx : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> sfx = new List<AudioClip>();
    [SerializeField]
    private AudioSource source;

    public void Play()
    {
        AudioClip randomSfx = sfx[Random.Range(0, sfx.Count)];
        source.PlayOneShot(randomSfx);
    }
}
