using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject root;
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button quitButton;

    private void Awake()
    {
        root.SetActive(false);
    }

    private void OnEnable()
    {
        resumeButton.onClick.AddListener(OnResume);
        quitButton.onClick.AddListener(OnQuit);
    }

    private void OnDisable()
    {
        resumeButton.onClick.RemoveListener(OnResume);
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

    private void OnQuit()
    {
        Application.Quit();
    }
}
