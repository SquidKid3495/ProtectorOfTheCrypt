using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "GridCell", menuName = "PathGeneration/Grid Cell")]
public class GridCellScriptableObject : ScriptableObject
{
    public enum CellType { Path, Ground, Empty, Water}

    public CellType cellType;
    public GameObject cellPrefab;
    public int yRotation;
}
