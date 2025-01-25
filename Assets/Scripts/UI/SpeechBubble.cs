using TMPro;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    private void Awake()
    {
        text.text = string.Empty;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowDialogue(string text)
    {
        gameObject.SetActive(true);
        this.text.text = text;
    }
}
