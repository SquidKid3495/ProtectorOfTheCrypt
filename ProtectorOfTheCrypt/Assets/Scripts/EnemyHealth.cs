using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _MaxHealth = 100;
    [SerializeField] private int _Health; // serialized just to see in inspector

    public int MaxHealth { get => _MaxHealth; private set => _MaxHealth = value; }
    public int CurrentHealth { get => _Health; private set => _Health = value; }

    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;

    private void OnEnable() 
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int Damage)
    {
        Debug.Log(Damage);
        int damageTaken = Mathf.Clamp(Damage, 0, CurrentHealth);
        CurrentHealth -= damageTaken;

        if(damageTaken != 0)
        {
            OnTakeDamage?.Invoke(damageTaken);
        }

        if(CurrentHealth == 0 & damageTaken != 0)
        {
            OnDeath?.Invoke(transform.position);
            Destroy(gameObject);
        }
    }
}
