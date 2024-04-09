using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{ 
    [SerializeField] private Button playGameButton;
    [SerializeField] private Button quitGameButton;

    private void Awake()
    {
        playGameButton.onClick.AddListener(LoadGame);
        quitGameButton.onClick.AddListener(QuitGame);
    }

    private void LoadGame()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}

