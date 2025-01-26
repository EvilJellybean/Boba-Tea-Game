using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryPanel : MonoBehaviour
{
    [SerializeField]
    private Button continueButton;

    [SerializeField]
    private string playSceneName = "PlayScene";

    private void OnEnable()
    {
        continueButton.onClick.AddListener(Continue);
    }

    private void OnDisable()
    {
        continueButton.onClick.RemoveListener(Continue);
    }

    private void Continue()
    {
        SceneManager.LoadScene(playSceneName);
    }
}
