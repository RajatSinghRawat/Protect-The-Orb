using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int RedSpiderPoints;
    [SerializeField] private int GraySpiderPoints;
    [SerializeField] private int BlueStripedSpiderPoints;
    [SerializeField] private int BlueWithOrangeSpotsSpiderPoints;
    [SerializeField] private int YellowSpiderPoints;
    [SerializeField] private UIManager UIManagerController;
    [SerializeField] private string KeyForHighScore;
    private int score;
    private int currentHighScore;

    private void Start()
    {
        GetHighScore();
    }

    private void Update()
    {
        CheckForHighScore();
    }

    public void increaseScore(EnemyType typeOfEnemy)
    {
        switch(typeOfEnemy)
        {
            case EnemyType.Blue_Striped_Spider:
                score += BlueStripedSpiderPoints;
                break;

            case EnemyType.BlueWithOrangeSpots_Spider:
                score += BlueWithOrangeSpotsSpiderPoints;
                break;

            case EnemyType.Gray_Spider:
                score += GraySpiderPoints;
                break;

            case EnemyType.Red_Spider:
                score += RedSpiderPoints;
                break;

            case EnemyType.Yellow_Spider:
                score += YellowSpiderPoints;
                break;
        }

        UIManagerController.UpdateScore(score);
    }

    public void CheckForHighScore()
    {
        if (score > currentHighScore)
        {
            currentHighScore = score;
            SaveHighScore(); // Save the new high score
        }
        UIManagerController.UpdateHighScore(currentHighScore);
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(KeyForHighScore, currentHighScore);
        PlayerPrefs.Save(); // Save the preferences to disk
    }

    private void GetHighScore()
    {
        if (PlayerPrefs.HasKey(KeyForHighScore))
        {
            currentHighScore = PlayerPrefs.GetInt(KeyForHighScore);
        }
        else
        {
            currentHighScore = 0;
        }
    }
}
