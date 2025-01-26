using System.Collections;
using TMPro;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    [SerializeField]
    private float delayPerCharacter = 0.02f;

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

        StopAllCoroutines();
        StartCoroutine(ShowAnimation(text));
    }

    private IEnumerator ShowAnimation(string text)
    {
        if(text.Length == 0)
        {
            yield break;
        }

        float totalTime = text.Length * delayPerCharacter;
        float t = 0;
        while (t < 1)
        {
            t = Mathf.Clamp01(t + Time.deltaTime / totalTime);

            int characters = Mathf.RoundToInt(text.Length * t);
            string visibleText = text.Substring(0, characters);
            this.text.text = visibleText;

            yield return null;
        }

        this.text.text = text;
    }
}
