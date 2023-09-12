using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        newTowerInstance.Spawn(TowerParent, this);
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
