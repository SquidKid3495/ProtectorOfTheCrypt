using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for moving the projectile and handeling collision. This script is set up to work with the ObjectPooling script.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody Rigidbody;

    private bool isTracking = false;
    private Transform target;
    private float speed;
    Vector3 direction;

    [field: SerializeField]
    public Vector3 SpawnLocation
    {
        get;
        private set;
    }
    [SerializeField]
    private float DelayedDisableTime = 10f;

    public delegate void CollisionEvent(Bullet Bullet, Collision Collision);
    public event CollisionEvent OnCollision;

    private void Awake() 
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // If the projectile is active: move towards the target
        if(isTracking)
        {
            if(target != null)
            {
                direction = target.position - transform.position;
                transform.position += direction.normalized * speed * Time.deltaTime;
            }
            else
            {
                transform.position += direction.normalized * speed * Time.deltaTime;
            }
            
        } 
    }

    /// <summary>
    /// Sets the projectile to the tower projectile spawn point and sets the target.
    /// </summary>
    /// <param name="Speed"></param>
    /// <param name="Target"></param>
    public void Spawn(float Speed, Transform Target, ElementType DamageType)
    {
        SpawnLocation = transform.position;
        speed = Speed;
        target = Target;
        isTracking = true;
        StartCoroutine(DelayedDisable(DelayedDisableTime));
    }

    /// <summary>
    /// If the projectile is active for "Time" without hitting an object: Disable it.
    /// </summary>
    /// <param name="Time"></param>
    /// <returns></returns>
    private IEnumerator DelayedDisable(float Time)
    {
        yield return new WaitForSeconds(Time);
        OnCollisionEnter(null);
    }

    private void OnCollisionEnter(Collision Collision) 
    {
        OnCollision?.Invoke(this, Collision);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;
        isTracking = false;
        target = null;
        OnCollision = null;
    }
}
