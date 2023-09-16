using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMonoBehaviour : MonoBehaviour
{
    public TowerScriptableObject tower;
    void Update()
    {
        tower.Shoot();
    }
}
