using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Needs a makeover for the wave manager.
/// </summary>
public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Vector3 enemyStartPoint;
    public int enemyDifficultyLevel;

    public int numOfEnemies = 10;
    public float enemySpawnDelay = 2f;


    public GameObject enemyObject;
    private List<Vector2Int> pathRoute;
    private List<Vector3> routeForEnemies = new List<Vector3>();
    private GameObject enemyInstance;

    public void SpawnWave()
    {
        StartCoroutine(SpawnEnemies());
    }

    public void SetPathCell(List<Vector2Int> pathRoute)
    {
        this.pathRoute = pathRoute;
        
        for(int i = 0; i < pathRoute.Count; i++)
        {
            routeForEnemies.Add(new Vector3(pathRoute[i].x, 0.5f, pathRoute[i].y));
        }
    }

    private void SpawnEnemy()
    {
        EnemySpawnData spawnData = new EnemySpawnData(routeForEnemies, enemyStartPoint,enemyDifficultyLevel);
        
        GameObject enemyObject = Instantiate(enemyPrefab, routeForEnemies[0], Quaternion.identity);
        Enemy enemyComponent = enemyObject.GetComponent<Enemy>();
        enemyComponent.Initialize(spawnData);
    }

    private IEnumerator SpawnEnemies()
    {
        while(numOfEnemies > 0)
        {
            numOfEnemies--;
            SpawnEnemy();
            yield return new WaitForSeconds(enemySpawnDelay);
        }
    }
}

public class EnemySpawnData
{
    public List<Vector3> points;
    public Vector3 startPoint;
    public int difficultyLevel;
    
    public EnemySpawnData(List<Vector3> points, Vector3 startPoint,int difficultyLevel)
    {
        this.points = points;
        this.startPoint = startPoint;
        this.difficultyLevel = difficultyLevel;
    }
}
