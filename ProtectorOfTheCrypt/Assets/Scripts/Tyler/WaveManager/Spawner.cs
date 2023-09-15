using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float TimeBetweenSpawning;
    public Vector3 SpawnPoint;

    public delegate void StoppedSpawning();
    public event StoppedSpawning StoppedSpawningObjects;

    public List<GameObject> SpawnedObjects = new List<GameObject>();
    public void SpawnGroup(Group group)
    {
        StartCoroutine(SpawnObject(group.Object, group.NumObjects));
    }

    int numSpawned = 0; // A Local Variable to keep track of our current spawn count.
    public IEnumerator SpawnObject(GameObject GameObjectToSpawn, int numToSpawn = 1)
    {
        while(numSpawned < numToSpawn)
        {
            yield return new WaitForSeconds(TimeBetweenSpawning);
            numSpawned++;
            GameObject Object = Instantiate(GameObjectToSpawn, gameObject.transform.position, Quaternion.identity);
            SpawnedObjects.Add(Object);
            // Initialize Enemy Data with Interface.
        }

        numSpawned = 0; // Reset.
        StoppedSpawningObjects?.Invoke();
    }
}
