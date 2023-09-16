using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Enemy", order = 0)]
public class EnemyScriptableObject : ScriptableObject 
{
    public GameObject ModelPrefab;
    public float BaseHealth = 10f;
    public float BaseSpeed = 1f;
    public WeaknessScriptableObject ElementType;
    public float WeaknessDamageMultiplier = 1.5f;

    private bool CanMove = true;
    private MonoBehaviour ActiveMonoBehaviour;
    private GameObject Model;
    private List<Vector3> Path = new List<Vector3>();

    public GameObject Spawn(Transform Parent, MonoBehaviour ActiveMonoBehaviour, List<Vector3> Path, Spawner Spawner)
    {
        this.ActiveMonoBehaviour = ActiveMonoBehaviour;
        this.Path = Path;

        Model = Instantiate(ModelPrefab);
        Model.transform.localPosition = Path[0];
        Model.AddComponent<EnemyMovementHandler>().Initialize(this, Path, BaseSpeed);

        EnemyHealth EnemyHealth = Model.AddComponent<EnemyHealth>();
        EnemyHealth.MaxHealth = BaseHealth;
        EnemyHealth.Element = ElementType;
        EnemyHealth._damageMultiplier = WeaknessDamageMultiplier;
        EnemyHealth._spawner = Spawner;

        return Model;
    }
}
