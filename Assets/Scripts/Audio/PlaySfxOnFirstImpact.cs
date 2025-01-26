using UnityEngine;

public class PlaySfxOnFirstImpact : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private float pitchRandomness = 0.25f;
    [SerializeField]
    private string tagRequired = "Liquid";
    [SerializeField]
    private float maxDelay = 2.5f;

    private bool playedSound;
    private float spawnTime;

    private void OnEnable()
    {
        spawnTime = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(playedSound)
        {
            return;
        }
        if(Time.time - spawnTime > maxDelay)
        {
            return;
        }
        if (!collision.transform.tag.Equals(tagRequired))
        {
            return;
        }
        playedSound = true;
        AudioManager.Instance.Play(clip, Random.Range(1 - pitchRandomness, 1 + pitchRandomness));
    }
}
