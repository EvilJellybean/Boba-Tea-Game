using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text score;

    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private string mainMenuScene = "MainMenu";

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(MainMenu);
    }

    private void OnDisable()
    {
        restartButton.onClick.RemoveListener(RestartGame);
        mainMenuButton.onClick.RemoveListener(MainMenu);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        score.text = Mathf.Round(CustomerManager.Instance.FinalScore * 100).ToString() + "%";
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
