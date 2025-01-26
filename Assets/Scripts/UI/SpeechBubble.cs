using System.Collections;
using TMPro;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    [SerializeField]
    private float startDelay = 0.5f;

    [SerializeField]
    private float delayPerCharacter = 0.02f;
    [SerializeField]
    private float sfxInterval = 0.1f;

    [SerializeField]
    private PlayRandomSfx playRandomSfx;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        text.text = string.Empty;
    }

    public void Hide()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    public void ShowDialogue(string text)
    {
        gameObject.SetActive(true);
        this.text.text = string.Empty;
        canvasGroup.alpha = 0;

        StopAllCoroutines();
        StartCoroutine(ShowAnimation(text));
    }

    private IEnumerator ShowAnimation(string text)
    {
        if(text.Length == 0)
        {
            yield break;
        }

        yield return new WaitForSeconds(startDelay);

        canvasGroup.alpha = 1;

        float sfxIntervalLeft = 0;

        float totalTime = text.Length * delayPerCharacter;
        float t = 0;
        while (t < 1)
        {
            t = Mathf.Clamp01(t + Time.deltaTime / totalTime);

            int characters = Mathf.RoundToInt(text.Length * t);
            string visibleText = text.Substring(0, characters);
            this.text.text = visibleText;

            sfxIntervalLeft -= Time.deltaTime;
            if(sfxIntervalLeft < 0)
            {
                sfxIntervalLeft = sfxInterval;
                playRandomSfx.Play();
            }

            yield return null;
        }

        this.text.text = text;
    }
}
