using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapVariablesSO", menuName = "Levels/MapVariables")]
public class MapVariablesSO : ScriptableObject
{
    public List<MapVariables> mapVariablesList = new List<MapVariables>();
}
