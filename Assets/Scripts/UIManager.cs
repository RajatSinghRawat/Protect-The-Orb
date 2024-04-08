using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

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
}
