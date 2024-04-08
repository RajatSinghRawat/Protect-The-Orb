using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform Minimum_Corner_Point;
    [SerializeField] private Transform Maximum_Corner_Point;
    [SerializeField] private SpawningRegions SpawningRegion;
    [SerializeField] private GameObject Orb;
    [SerializeField] private List<Enemy> enemies = new List<Enemy>();
    [SerializeField] private float offSet;
    [SerializeField] private float minimumAssignedTime; 
    [SerializeField] private float maximumAssignedTime;
    [SerializeField] private float waitTimeForTimeDecreaser;
    [SerializeField] private ScoreManager scoreManagerController;
    private Vector3 randomPosition;
    private float pointOfFixedAxis;
    private float minimumPointOfVariableAxis, maximumPointOfVariableAxis;
    private int enemy;
    private float assignedTime;
    

    //getters
    public float getPointOfFixedAxis()
    {
        return pointOfFixedAxis;
    }   

    public float getMinimumPointOfVariableAxis() 
    {  
        return minimumPointOfVariableAxis; 
    }

    public float getMaximumPointOfVariableAxis() 
    { 
        return maximumPointOfVariableAxis; 
    }

    // Start is called before the first frame update
    void Start()
    {
        switch(SpawningRegion)
        {
            case SpawningRegions.Top:
                AssignPointsToSpawner(Minimum_Corner_Point.position.y, Minimum_Corner_Point.position.x, Maximum_Corner_Point.position.x);
                break;

            case SpawningRegions.Bottom:
                AssignPointsToSpawner(Minimum_Corner_Point.position.y, Minimum_Corner_Point.position.x, Maximum_Corner_Point.position.x);
                break;

            case SpawningRegions.Left:
                AssignPointsToSpawner(Minimum_Corner_Point.position.x, Minimum_Corner_Point.position.y, Maximum_Corner_Point.position.y);
                break;

            case SpawningRegions.Right:
                AssignPointsToSpawner(Minimum_Corner_Point.position.x, Minimum_Corner_Point.position.y, Maximum_Corner_Point.position.y);
                break;
        }

        StartSpawning();
        StartCoroutine(DecreaseMinimumAssignedTime());
        StartCoroutine(DecreaseMaximumAssignedTime());
    }

    private void StartSpawning()
    {
        assignedTime = Random.Range(minimumAssignedTime, maximumAssignedTime);
        StartCoroutine(SpawnAfterAssignedTime());
    }

    private IEnumerator SpawnAfterAssignedTime()
    {
        yield return new WaitForSeconds(assignedTime);
        if(GameManager.Instance.getMaximumNumberOfEnemies() > GameManager.Instance.getNumberOfEnemies())
        {
            SelectEnemy();
            spawnEnemy();
            GameManager.Instance.AddOneEnemy();
        } 
        else
        {
            StartSpawning();
        }
    }

    private IEnumerator DecreaseMaximumAssignedTime()
    {
        yield return new WaitForSeconds(waitTimeForTimeDecreaser);
        DecreaseMaximumTime();
    }

    private void DecreaseMaximumTime()
    {
        if (maximumAssignedTime > 2)
        {
            maximumAssignedTime--;
            StartCoroutine(DecreaseMaximumAssignedTime());
        }
        else
        {
            StopCoroutine(DecreaseMaximumAssignedTime());
        }
    }

    private IEnumerator DecreaseMinimumAssignedTime()
    {
        yield return new WaitForSeconds(waitTimeForTimeDecreaser);
        DecreaseMinimumTime();
    }

    private void DecreaseMinimumTime()
    {
        if (minimumAssignedTime > 1)
        {
            minimumAssignedTime--;
            StartCoroutine(DecreaseMinimumAssignedTime());
        }
        else
        {
            StopCoroutine(DecreaseMinimumAssignedTime());
        }
    }

    private void SelectEnemy()
    {
        enemy = Random.Range(0, enemies.Count);
    }

    private void PickRandomPosition(SpawningRegions Side)
    {
        if(Side == SpawningRegions.Top || Side == SpawningRegions.Bottom)
        {
            randomPosition.y = pointOfFixedAxis;
            randomPosition.x = Random.Range(minimumPointOfVariableAxis, maximumPointOfVariableAxis);
        }
        else if (Side == SpawningRegions.Left || Side == SpawningRegions.Right)
        {
            randomPosition.x = pointOfFixedAxis;
            randomPosition.y = Random.Range(minimumPointOfVariableAxis, maximumPointOfVariableAxis);
        }
    }

    public void spawnEnemy()
    {
        PickRandomPosition(SpawningRegion);
        Enemy spawnedEnemy = Instantiate(enemies[enemy]);
        Vector2 direction = (randomPosition - Orb.transform.position).normalized;
        spawnedEnemy.SetTransform(randomPosition, direction, offSet);
        spawnedEnemy.SetScoreManagerReference(scoreManagerController);
        StartSpawning();
    }

    private void AssignPointsToSpawner(float fixedPoint, float min_Variable_Point, float max_Variable_Point)
    {
        pointOfFixedAxis = fixedPoint;
        minimumPointOfVariableAxis = min_Variable_Point;
        maximumPointOfVariableAxis = max_Variable_Point;
    }   
}

public enum SpawningRegions
{
    Top,
    Bottom,
    Left,
    Right
}