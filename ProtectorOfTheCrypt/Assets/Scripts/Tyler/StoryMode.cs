using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryMode : GameMode
{
    public WaveManager waveManager;

    public override bool CheckGameWon()
    {
        return waveManager.state == WaveManager.SpawnState.FINISHED
               && waveManager.EnemySpawner.SpawnedObjects.Count == 0;
    }

    public override bool CheckGameLost()
    {
        return GameManager.instance.Souls == 0;
    }
}
