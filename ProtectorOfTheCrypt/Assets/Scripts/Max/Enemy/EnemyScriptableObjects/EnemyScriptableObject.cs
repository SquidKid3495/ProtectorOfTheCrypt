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
    private EnemyHealth EnemyHealth;
    private List<Vector3> Path = new List<Vector3>();
    private Vector3 Target;
    private int WaypointIndex = 1;

    public void Spawn(Transform Parent, MonoBehaviour ActiveMonoBehaviour, List<Vector3> Path)
    {
        this.ActiveMonoBehaviour = ActiveMonoBehaviour;
        this.Path = Path;

        Model = Instantiate(ModelPrefab);
        Target = Path[0];
        Model.transform.localPosition = Target;

        EnemyHealth = Model.AddComponent<EnemyHealth>();
        EnemyHealth._damageMultiplier = WeaknessDamageMultiplier;
    }

    public void Move()
    {
        if (CanMove)
        {
            Vector3 dir = Target - Model.transform.position;
            dir.Normalize();

            Model.transform.Translate(dir * BaseSpeed * Time.deltaTime);

            if (Vector3.Distance(Model.transform.position, Target) <= 0.4f)
            {
                WaypointIndex++;
                if (WaypointIndex >= Path.Count)
                {
                    // Reached the last waypoint, stop moving
                    CanMove = false;
                    return;
                }

                Target = Path[WaypointIndex];
            }
        }
    }
}
