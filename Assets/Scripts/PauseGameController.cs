using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGameController : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button restartGameButton;
    [SerializeField] private BarrelController barrelControllerObject;

    private void Awake()
    {
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
        restartGameButton.onClick.AddListener(RestartGame);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        barrelControllerObject.enabled = true;
        gameObject.SetActive(false);
    }

    private void RestartGame()
    {
        GameManager.Instance.ResetNumberOfEnemies();
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    private void LoadMainMenu()
    {
        GameManager.Instance.ResetNumberOfEnemies();
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene - 1);
    }
}
