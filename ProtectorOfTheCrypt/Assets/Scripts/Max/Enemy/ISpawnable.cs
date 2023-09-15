using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for Spawnable. Tells Spawnable Objets how they will be spawned.
/// </summary>
public interface ISpawnable
{
    public delegate void SpawnEvent(Vector3 Position);
    public event SpawnEvent OnSpawn;

    public void Spawn();
}
