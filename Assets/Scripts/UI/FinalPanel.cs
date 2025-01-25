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

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        restartButton.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        restartButton.onClick.RemoveListener(RestartGame);
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
}
