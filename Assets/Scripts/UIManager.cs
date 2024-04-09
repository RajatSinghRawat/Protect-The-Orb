using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private PauseGameController pauseGameObject;
    [SerializeField] private GameOverController gameOverObject;

    //Update the score on display
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    //Update the highscore on display
    public void UpdateHighScore(int score)
    {
        highScoreText.text = score.ToString();
    }

    public void setPauseScreenActive()
    {
        pauseGameObject.gameObject.SetActive(true);
    }

    public void setGameOverScreenActive()
    {
        gameOverObject.gameObject.SetActive(true);
    }
}
