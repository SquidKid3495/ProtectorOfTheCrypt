using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Each enemy should have a reference 
/// </summary>
[CreateAssetMenu(fileName = "EnemyWeakness", menuName = "Enemies/Weakness", order = 0)]
public class WeaknessScriptableObject : ScriptableObject
{
    /// <summary>
    /// The types of element the enemy is weak to
    /// </summary>


    public TowerType[] Weaknesses;
}
