using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

/// <summary>
/// Contains a DamageCurve. You can use the curve to add a damage falloff or the constant for a set damage.
/// </summary>
[CreateAssetMenu(fileName = "Damage Config", menuName = "Towers/Damage Config", order = 1)]
public class DamageConfigScriptableObject : ScriptableObject
{
    public MinMaxCurve DamageCurve;

    private void Reset()
    {
        DamageCurve.mode = ParticleSystemCurveMode.Curve;
    }

    public int GetDamage(float Distance = 0)
    {
        return Mathf.CeilToInt(DamageCurve.Evaluate(Distance, Random.value));
    }
}
