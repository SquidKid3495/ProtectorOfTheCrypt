using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementHandler : MonoBehaviour
{
    private EnemyScriptableObject enemy = null;

    private bool canMove;
    private List<Vector3> path = new List<Vector3>();
    private Vector3 target;
    private int waypointIndex = 0;
    public float baseSpeed;
    public void Initialize(EnemyScriptableObject EnemyToSet, List<Vector3> Path, float BaseSpeed)
    {
        enemy = EnemyToSet;
        path = Path;
        target = path[1];
        baseSpeed = BaseSpeed;
        canMove = true;
    }

    private void Update()
    {
        if(canMove)
            Move();
    }

    public void Move()
    {
        Vector3 dir = target - transform.position;
        dir.Normalize();
        transform.Translate(dir * baseSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) <= 0.1f)
        {
            waypointIndex++;
            if (waypointIndex >= path.Count)
            {
                // Reached the last waypoint, stop moving
                canMove = false;
                return;
            }
            target = path[waypointIndex];
        }
    }
}
