using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class WaveManager : MonoBehaviour
{
    [Header("Spawners")]
    public Spawner EnemySpawner;

    public delegate void WaveStarted();
    public static event WaveStarted WaveStartDisplay;

    public delegate void WaveEnded();
    public static event WaveStarted WaveEndDisplay;

    [System.Serializable]
    public class Wave
    {
        public Group EnemyGroup;
        [Range(0f, 20f)]
        public float TimeUntilNextWave;
    }
    public List<Wave> WavesToSpawn = new List<Wave>();
    public Wave CurrentWave;
    private int CurrentWaveCount = -1;

    public enum SpawnState { SPAWNING, WAITING, GRACE_PERIOD};
    public SpawnState state = SpawnState.SPAWNING;


    private float searchCountdown = 1f;

    private void Awake()
    {
        EnemySpawner = GetComponent<Spawner>();
        EnemySpawner.StoppedSpawningObjects += () => SetToWait();
    }

    private void OnEnable()
    {
        SpawnWave();
    }

    private void Update()
    {
        HandleWaves();
    }

    private void HandleWaves()
    {
        if (state == SpawnState.WAITING)
        {
            if (WaveOver())
            {
                WaveCompleted();
            }
        }
    }

    private void SpawnWave()
    {
        EnemySpawner.SpawnedObjects.Clear();
        CurrentWave = WavesToSpawn[++CurrentWaveCount];

        state = SpawnState.SPAWNING;
        EnemySpawner.SpawnGroup(CurrentWave.EnemyGroup);

        WaveStartDisplay?.Invoke();
    }
    private void SetToWait()
    {
        state = SpawnState.WAITING;
    }
    private void WaveCompleted()
    {
        state = SpawnState.GRACE_PERIOD;
        Invoke(nameof(SpawnWave), CurrentWave.TimeUntilNextWave);
    }
    public bool WaveOver()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (EnemySpawner.SpawnedObjects.Count() == 0)
            {
                return true;
            }
        }
        return false;
    }
}
