using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Interfaces with the IDamageable script, communicates with other enemy scripts about damage taken and dying.
/// </summary>
public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float _MaxHealth = 100;
    [SerializeField] private float _Health; // serialized just to see in inspector
    [SerializeField] private WeaknessScriptableObject Element;
    public float _damageMultiplier = 10f;

    public float MaxHealth { get => _MaxHealth; private set => _MaxHealth = value; }
    public float CurrentHealth { get => _Health; private set => _Health = value; }

    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;

    private void OnEnable() 
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float Damage, ElementType DamageType)
    {
        float damageTaken = Damage * CompareElementTypes(DamageType);
        // Makes sure the current health is never negative
        damageTaken = Mathf.Clamp(damageTaken, 0, CurrentHealth);
        CurrentHealth -= damageTaken;

        if(damageTaken != 0) // Damage
        {
            OnTakeDamage?.Invoke(damageTaken);
        }

        if(CurrentHealth == 0 & damageTaken != 0) // Death
        {
            OnDeath?.Invoke(transform.position);
            Destroy(gameObject);
        }
    }

    private float CompareElementTypes(ElementType DamageType)
    {
        foreach(ElementType element in Element.Weaknesses) 
        {
            if(element == DamageType)
            {
                return _damageMultiplier;
            }
        }
        return 1f;
    }
}
