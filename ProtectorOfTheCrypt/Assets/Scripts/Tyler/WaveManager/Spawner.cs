using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float TimeBetweenSpawning;
    public List<Vector3> Path = new List<Vector3>();

    public delegate void StoppedSpawning();
    public event StoppedSpawning StoppedSpawningObjects;

    public List<EnemyScriptableObject> SpawnedObjects = new List<EnemyScriptableObject>();
    public void SpawnGroup(Group group)
    {
        StartCoroutine(SpawnObject(group.Object, group.NumObjects));
    }

    int numSpawned = 0; // A Local Variable to keep track of our current spawn count.
    public IEnumerator SpawnObject(EnemyScriptableObject EnemyToSpawn, int numToSpawn = 1)
    {
        while(numSpawned < numToSpawn)
        {
            yield return new WaitForSeconds(TimeBetweenSpawning);
            numSpawned++;
            EnemyToSpawn.Spawn(transform, this, Path);
            SpawnedObjects.Add(EnemyToSpawn);
            // Initialize Enemy Data with Interface.
        }

        numSpawned = 0; // Reset.
        StoppedSpawningObjects?.Invoke();
    }



    #region EnemyScriptableObjectHandling
    private void Update() 
    {
        foreach(EnemyScriptableObject enemy in SpawnedObjects)
        {
            enemy.Move();
        }
    }
    #endregion
}
