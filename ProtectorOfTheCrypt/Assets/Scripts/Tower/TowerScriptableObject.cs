using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Tower", menuName = "Towers/Tower", order = 0)]
public class TowerScriptableObject : ScriptableObject 
{
    public ImpactType ImpactType;
    public TowerType Type;
    public string Name;
    public GameObject ModelPrefab;
    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;

    public DamageConfigScriptableObject DamageConfig;
    public ProjectileConfigurationScriptableObject ProjectileConfig;
    public TrailConfigurationScriptableObject TrailConfig;
    public AuidoConfigScriptableObject AudioConfig;

    
    private MonoBehaviour ActiveMonoBehaviour;
    private GameObject Model;
    private GameObject ProjectileSpawnpoint;
    private AudioSource ShootingAudioSource;
    private float LastShootTime;
    private ParticleSystem ShootSystem;
    private ObjectPool BulletPool;
    private ObjectPool<TrailRenderer> TrailPool;

    public float range = 20f;
    private GameObject closestEnemy;
    
    public void Spawn(Transform Parent, MonoBehaviour ActiveMonoBehaviour)
    {
        this.ActiveMonoBehaviour = ActiveMonoBehaviour;
        LastShootTime = 0;
        TrailPool = new ObjectPool<TrailRenderer>(CreateTrail);
        if(!ProjectileConfig.IsHitscan)
        {
            BulletPool = ObjectPool.CreateInstance(ProjectileConfig.BulletPrefab.GetComponent<PoolableObject>(), 10);
        }

        Model = Instantiate(ModelPrefab);
        Model.transform.SetParent(Parent, false);
        Model.transform.localPosition = SpawnPoint;
        Model.transform.localRotation = Quaternion.Euler(SpawnRotation);

        ProjectileSpawnpoint = Model.transform.Find("ProjectileSpawnpoint").gameObject;

        ShootSystem = ProjectileSpawnpoint.GetComponent<ParticleSystem>();
        ShootingAudioSource = Model.GetComponent<AudioSource>();
    }
    
    public void Despawn()
    {
        Destroy(Model);
        Destroy(this);
    }

    public void Shoot()
    {
        if(Time.time > ProjectileConfig.FireRate + LastShootTime)
        {
            Vector3 shootDirection;
            bool canShoot = true;
            if(closestEnemy == null || Vector3.Distance(ShootSystem.transform.position, closestEnemy.transform.position) >= range)
            {
                FindClosestEnemy(out shootDirection, out canShoot);
            }
            else
            {
                shootDirection = (closestEnemy.transform.position - ShootSystem.transform.position).normalized;
            }
            if(canShoot)
            {
                //AudioConfig.PlayShootingClip(ShootingAudioSource);
                LastShootTime = Time.time;
                ShootSystem.Play();
                
                if(ProjectileConfig.IsHitscan)
                {
                    if(shootDirection != Vector3.zero)
                        DoHitscanShoot(shootDirection);
                }
                else
                {
                    
                    if(shootDirection != Vector3.zero)
                        DoProjectileShoot(shootDirection);
                }
            }
        }
    }

    
    private void DoProjectileShoot(Vector3 ShootDirection)
    {
        Bullet bullet = BulletPool.GetObject().GetComponent<Bullet>();
        bullet.gameObject.SetActive(true);
        bullet.OnCollision += HandleBulletCollision;
        bullet.transform.position = ShootSystem.transform.position;
        bullet.Spawn(ProjectileConfig.BulletSpeed,closestEnemy.transform);

        TrailRenderer trail = TrailPool.Get();
        if(trail != null)
        {
            trail.transform.SetParent(bullet.transform, false);
            trail.transform.localPosition = Vector3.zero;
            trail.emitting = true;
            trail.gameObject.SetActive(true);
        }
    }

    private void HandleBulletCollision(Bullet Bullet, Collision Collision)
    {
        TrailRenderer trail = Bullet.GetComponentInChildren<TrailRenderer>();
        if(trail != null)
        {
            trail.transform.SetParent(null, true);
            ActiveMonoBehaviour.StartCoroutine(DelayedDisableTrail(trail));
        }

        Bullet.gameObject.SetActive(false);
        BulletPool.ReturnObjectToPool(Bullet.GetComponent<PoolableObject>());

        if(Collision != null)
        {
            ContactPoint contactPoint = Collision.GetContact(0);
            
            HandleBulletImpact(
                Vector3.Distance(contactPoint.point, Bullet.SpawnLocation),
                contactPoint.point,
                contactPoint.normal,
                contactPoint.otherCollider
            );
        }
    }

    private void HandleBulletImpact(float DistanceTraveled, Vector3 HitLocation, Vector3 HitNormal, Collider HitCollider)
    {
        SurfaceManager.Instance.HandleImpact(
            HitCollider.gameObject,
            HitLocation,
            HitNormal,
            ImpactType,
            0
        );

        if(HitCollider.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(DamageConfig.GetDamage(DistanceTraveled));
        }
    }

    private IEnumerator DelayedDisableTrail(TrailRenderer Trail)
    {
        yield return new WaitForSeconds(TrailConfig.Duration);
        yield return null;
        Trail.emitting = false;
        Trail.gameObject.SetActive(false);
        TrailPool.Release(Trail);
    }

    private void DoHitscanShoot(Vector3 ShootDirection)
    {
        if(Physics.Raycast(
            ShootSystem.transform.position,
            ShootDirection,
            out RaycastHit hit,
            float.MaxValue,
            ProjectileConfig.HitMask))
        {
            ActiveMonoBehaviour.StartCoroutine(
                PlayTrail(
                    ShootSystem.transform.position,
                    hit.point,
                    hit));
        }
        else
        {
            ActiveMonoBehaviour.StartCoroutine(
                PlayTrail(
                    ShootSystem.transform.position,
                    ShootSystem.transform.position + (ShootDirection * TrailConfig.MissDistance),
                    new RaycastHit()));
        }
    }

    private void FindClosestEnemy(out Vector3 directionToEnemy, out bool targetInRange)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float closestDistance = Mathf.Infinity;
        directionToEnemy = Vector3.zero;
        targetInRange = false;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(ShootSystem.transform.position, enemy.transform.position);

            if (distanceToEnemy < closestDistance && distanceToEnemy <= range)
            {
                closestEnemy = enemy;
                closestDistance = distanceToEnemy;
                directionToEnemy = (closestEnemy.transform.position - ShootSystem.transform.position).normalized;
                targetInRange = true;
            }
        }
        
    }

    private IEnumerator PlayTrail(Vector3 StartPoint, Vector3 EndPoint, RaycastHit Hit)
    {
        TrailRenderer instance = TrailPool.Get();
        instance.gameObject.SetActive(true);
        instance.transform.position = StartPoint;
        yield return null; // avoid position carry-over from last frame

        instance.emitting = true;

        float distance = Vector3.Distance(StartPoint, EndPoint);
        float remainingDistance = distance;
        while(remainingDistance > 0)
        {
            instance.transform.position = Vector3.Lerp(
                StartPoint,
                EndPoint,
                Mathf.Clamp01(1 - (remainingDistance / distance))
            );
            remainingDistance -= TrailConfig.SimulationSpeed * Time.deltaTime;

            yield return null;
        }
        instance.transform.position = EndPoint;

        if(Hit.collider != null)
        {
            HandleBulletImpact(distance, EndPoint, Hit.normal, Hit.collider);
        }

        yield return new WaitForSeconds(TrailConfig.Duration);
        yield return null;
        instance.emitting = false;
        instance.gameObject.SetActive(false);
        TrailPool.Release(instance);
    }

    private TrailRenderer CreateTrail()
    {
        GameObject instance = new GameObject("Bullet Trail");
        TrailRenderer trail = instance.AddComponent<TrailRenderer>();
        trail.colorGradient = TrailConfig.Color;
        trail.material = TrailConfig.Material;
        trail.widthCurve = TrailConfig.WidthCurve;
        trail.time = TrailConfig.Duration;
        trail.minVertexDistance = TrailConfig.MinVertexDistance;

        trail.emitting = false;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        return trail;
    }

    private Bullet CreateBullet()
    {
        return Instantiate(ProjectileConfig.BulletPrefab);
    }
}

