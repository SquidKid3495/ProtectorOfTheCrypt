using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class BountyManager : MonoBehaviour
{
    //public ScenesData sceneDB;

    public static BountyManager instance;

    [Header("Debug")]
    public bool OnlySpawnSpecialEnemies;
    public bool CanSpawnFlagship;

    [Header("ObjectsToSpawn")]
    public GameObject warpGatePrefab;
    public GameObject flagshipPrefab;
    public GameObject ancientTotemPrefab;

    [Header("Spawned Objects")]
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    [Header("Spawners")]
    public GameObject EnemySpawner;

    public delegate void WaveStarted();
    public static event WaveStarted WaveStartDisplay;

    public delegate void WaveEnded();
    public static event WaveStarted WaveEndDisplay;

    [System.Serializable]
    public class Wave
    {
        //public EnemyGroup enemyGroup;
        //public int count;
        //public int waveNumber;
        //public List<ObjectSpawner> activeSpawnBoxes;
        //public float startTime;
        //public int accumulatedCost;
    }
    public Wave[] Waves;
    public Wave CurrentWave;
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    public SpawnState state = SpawnState.COUNTING;


    [Range(1f, 1.20f)]
    public float enemySpawnMultiplier;
    public float maxWaveLength;

    private float searchCountdown = 1f;

    public void Awake()
    {
    }
    public void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }
    private void Update()
    {
        HandleEnemyWaves();
    }

    private float waveCountdown; // Used for keeping track of wave timings.
    private void HandleEnemyWaves()
    {
        //if (state == SpawnState.WAITING)
        //{
        //    if (WaveOver())
        //    {
        //        WaveCompleted();
        //        return;
        //    }

        //    else
        //        return;
        //}
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                SpawnWave(CreateWave());
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    private Wave CreateWave()
    {
        Wave wave = new Wave();
        //wave.waveNumber = waveCount;
        //wave.count = baseEnemies + (int)(1f + Mathf.Pow(enemySpawnMultiplier, wave.waveNumber));
        //wave.activeSpawnBoxes = GetRandomSpawnBoxes();
        //wave.startTime = Time.time;
        //waveCount++;
        return wave;
    }

    private void SpawnWave(Wave _wave)
    {
        //currentWave = _wave;
        //state = SpawnState.SPAWNING;

        //for (int i = 0; i < _wave.count; i++)
        //{
        //    if (!CanAddCost(0))
        //        break;
        //    SpawnEnemy(_wave);
        //}
        //WaveStartDisplay();

        //state = SpawnState.WAITING;
    }

    private void WaveCompleted()
    {
        //state = SpawnState.COUNTING;
        //if (WaveEndDisplay != null)
        //    WaveEndDisplay();
        //waveCountdown = timeBetweenWaves;
    }
    //public bool WaveOver()
    //{
    //    //searchCountdown -= Time.deltaTime;
    //    //if (searchCountdown <= 0f)
    //    //{
    //    //    searchCountdown = 1f;
    //    //    if ((spawnedEnemies.Count() <= enemiesRequiredForNextRound) ||
    //    //        (Time.time - currentWave.startTime) >= maxWaveLength)
    //    //    {
    //    //        return true;
    //    //    }
    //    //}
    //    //return false;
    //}

    void SpawnEnemyGroup(Wave wave)
    {
        //ObjectSpawner spawner = wave.activeSpawnBoxes[UnityEngine.Random.Range(0, wave.activeSpawnBoxes.Count)];
        //CalculateSpawnChances();
        //GameObject spawnedEnemy = spawner.SpawnEnemy(sceneDB.currentLevel.PickRandomEnemy());
        //spawnedEnemies.Add(spawnedEnemy);
    }
}
