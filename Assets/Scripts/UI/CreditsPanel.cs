using UnityEngine;

public class CreditsPanel : MonoBehaviour
{
    private static CreditsPanel instance;
    public static CreditsPanel Instance => instance;

    private void Awake()
    {
        instance = this;
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
