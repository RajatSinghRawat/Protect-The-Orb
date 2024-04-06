using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int maximumEnemies;
    [SerializeField] int numberOfEnemies;
    private static GameManager instance;

    //getters
    public int getNumberOfEnemies()
    {
        return numberOfEnemies;
    }

    public int getMaximumNumberOfEnemies()
    {
        return maximumEnemies;
    }  

    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddOneEnemy()
    {
        numberOfEnemies++;
    }  

    public void RemoveOneEnemy()
    {
        numberOfEnemies--;
    }
}
