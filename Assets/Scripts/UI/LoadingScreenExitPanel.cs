using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenExitPanel : MonoBehaviour
{
    private static LoadingScreenExitPanel instance;
    public static LoadingScreenExitPanel Instance => instance;

    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private float loadDelay = 0.6f;

    private void Awake()
    {
        panel.SetActive(false);
        instance = this;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(_LoadScene(sceneName));
    }

    private IEnumerator _LoadScene(string sceneName)
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(sceneName);
    }
}
