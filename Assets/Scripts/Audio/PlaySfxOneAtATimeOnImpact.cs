using UnityEngine;

public class PlaySfxOneAtATimeOnImpact : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private float pitchRandomness = 0.25f;

    private bool playedSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playedSound)
        {
            return;
        }
        playedSound = true;
        AudioManager.Instance.PlayOneAtATime(clip, Random.Range(1 - pitchRandomness, 1 + pitchRandomness));
    }
}
