using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Initialization")]
    public List<Vector3> points;
    private Vector3 startPoint;
    protected int difficulty;
    private float speed = 1;

    [Header("Health")]
    public EnemyHealth Health;

    [Header("Pathing")]
    public bool canMove;
    private Vector3 target;
    private int waypointIndex;

    // Start is called before the first frame update
    private void Start()
    {
        Health.OnDeath += Die;
    }
    private void Update() 
    {
        if (canMove)
        {
            Vector3 dir = target - transform.position;
            dir.Normalize();

            transform.Translate(dir * speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) <= 0.4f)
            {
                waypointIndex++;
                if (waypointIndex >= points.Count)
                {
                    // Reached the last waypoint, stop moving
                    canMove = false;
                    return;
                }

                target = points[waypointIndex];
            }
        }
    }
    public void Initialize(EnemySpawnData spawnData)
    {
        points = spawnData.points;
        startPoint = spawnData.startPoint;
        difficulty = spawnData.difficultyLevel;
        target = points[0];
        canMove = true;
    }

    private void Die(Vector3 Position)
    {
        Destroy(gameObject);
        Destroy(this);
    }
}
