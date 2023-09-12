using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Config", menuName = "Towers/Projectile Configuration", order = 2)]
public class ProjectileConfigurationScriptableObject : ScriptableObject
{
    public bool IsHitscan = true;
    public Bullet BulletPrefab;
    public float BulletSpeed = 1f;
    public LayerMask HitMask;
    public float FireRate = 0.25f;
}
