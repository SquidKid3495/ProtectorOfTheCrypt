using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

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
    [HideInInspector]
    public int CurrentWaveCount = -1;

    public enum SpawnState { SPAWNING, WAITING, FINISHED};
    public SpawnState state = SpawnState.WAITING;


    private void Awake()
    {
        EnemySpawner = GetComponent<Spawner>();
        EnemySpawner.StoppedSpawningObjects += () => WaveCompleted();

        if(GameManager.instance.GameMode is StoryMode)
        {
            StoryMode storyMode = GameManager.instance.GameMode as StoryMode;
            storyMode.waveManager = this;
        }
    }

    

    private void OnEnable()
    {
        SpawnWave();
    }

    private void OnDisable()
    {
        EnemySpawner.StoppedSpawningObjects -= () => WaveCompleted();
    }

    private void SpawnWave()
    {
        CurrentWave = WavesToSpawn[++CurrentWaveCount];

        state = SpawnState.SPAWNING;
        EnemySpawner.SpawnGroup(CurrentWave.EnemyGroup);

        WaveStartDisplay?.Invoke();
    }

    private void WaveCompleted()
    {
        WaveEndDisplay?.Invoke();

        if (CurrentWaveCount + 1 >= WavesToSpawn.Count)
        {
            state = SpawnState.FINISHED; 
            return;
        }
        else
        {
            state = SpawnState.WAITING;
        }

        Invoke(nameof(SpawnWave), CurrentWave.TimeUntilNextWave);
        
    }
}
