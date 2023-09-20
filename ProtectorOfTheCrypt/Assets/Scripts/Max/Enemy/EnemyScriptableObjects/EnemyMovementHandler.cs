using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementHandler : MonoBehaviour
{
    private EnemyScriptableObject enemy = null;

    private bool paused;
    private List<Vector3> path = new List<Vector3>();
    private Vector3 target;
    private int waypointIndex = 0;
    public float baseSpeed;
    public void Awake()
    {
        GameManager.instance.OnGamePaused += UpdateGamePaused;
    }
    public void Initialize(EnemyScriptableObject EnemyToSet, List<Vector3> Path, float BaseSpeed)
    {
        enemy = EnemyToSet;
        path = Path;
        target = path[1];
        baseSpeed = BaseSpeed;
    }

    private void Update()
    {
        
            Move();
    }

    public void Move()
    {
        if(paused)
            return;
        Vector3 dir = target - transform.position;
        dir.Normalize();
        transform.Translate(dir * baseSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) <= 0.1f)
        {
            waypointIndex++;
            if (waypointIndex >= path.Count)
            {
                // Reached the last waypoint, stop moving
                GameManager.instance.RemoveSouls(5);
                Destroy(gameObject);
                return;
            }
            target = path[waypointIndex];
        }
    }

    private void UpdateGamePaused(bool isPaused)
    {
        paused = isPaused;
    }
    private void OnDestroy()
    {
        GameManager.instance.OnGamePaused -= UpdateGamePaused;
    }
}
