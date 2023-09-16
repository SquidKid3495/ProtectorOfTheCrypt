using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This is currently the script that places the tower. Max will completely redo this script to better fit our style of tower placement.
/// </summary>
[DisallowMultipleComponent]
public class TowerProjectileSelector : MonoBehaviour
{
    [SerializeField] private TowerType Tower;
    [SerializeField] private Transform TowerParent;
    [SerializeField] private List<TowerScriptableObject> Towers;

    [Space]
    [Header("Runtime Filled")]
    public TowerScriptableObject ActiveTower;

    private void Start()
    {
        SpawnTower();
    }
    private void Update()
    {
        ActiveTower.Shoot();
    }

    public void SpawnTower()
    {
        TowerScriptableObject tower = Towers.Find(t => t.Type == Tower);

        if (tower == null)
        {
            Debug.LogError($"No TowerScriptableObject found for TowerType: {Tower}");
            return;
        }

        TowerScriptableObject newTowerInstance = CreateNewTowerInstance(tower);
        ActiveTower = newTowerInstance;
        newTowerInstance.Spawn();
    }

    private TowerScriptableObject CreateNewTowerInstance(TowerScriptableObject originalTower)
    {
        TowerScriptableObject newTowerInstance = Instantiate(originalTower);
        // Set any additional properties or configurations for the new tower instance if needed
        return newTowerInstance;
    }

    public void DespawnTower()
    {
        ActiveTower.Despawn();
    }
}
