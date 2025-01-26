using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject root;
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private string mainMenuScene = "MainMenu";

    private void Awake()
    {
        root.SetActive(false);
    }

    private void OnEnable()
    {
        resumeButton.onClick.AddListener(OnResume);
        mainMenuButton.onClick.AddListener(OnMainMenu);
        quitButton.onClick.AddListener(OnQuit);
    }

    private void OnDisable()
    {
        resumeButton.onClick.RemoveListener(OnResume);
        mainMenuButton.onClick.RemoveListener(OnMainMenu);
        quitButton.onClick.RemoveListener(OnQuit);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            root.SetActive(!root.activeSelf);
        }
    }

    private void OnResume()
    {
        root.SetActive(false);
    }

    private void OnMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    private void OnQuit()
    {
        Application.Quit();
    }
}
