using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private BarrelController barrelControllerObject;

    private void Awake()
    {
        restartButton.onClick.AddListener(ReloadLevel);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    private void Start()
    {
        barrelControllerObject.enabled = false;
    }

    private void ReloadLevel()
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
