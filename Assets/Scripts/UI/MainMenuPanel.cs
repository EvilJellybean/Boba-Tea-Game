using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button creditsButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private string playSceneName = "PlayScene";

    private void OnEnable()
    {
        playButton.onClick.AddListener(PlayButton_OnClick);
        creditsButton.onClick.AddListener(CreditsButton_OnClick);
        quitButton.onClick.AddListener(QuitButton_OnClick);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(PlayButton_OnClick);
        creditsButton.onClick.RemoveListener(CreditsButton_OnClick);
        quitButton.onClick.RemoveListener(QuitButton_OnClick);
    }

    private void PlayButton_OnClick()
    {
        SceneManager.LoadScene(playSceneName);
    }

    private void CreditsButton_OnClick()
    {
        CreditsPanel.Instance.Show();
    }

    private void QuitButton_OnClick()
    {
        Application.Quit();
    }
}
