using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridWidth = 10;
    public int gridHeight = 8;
    public int minPathLength = 15;

    /// <Summary> Enemy Manager relays the information from the grid and gives it to the enemies</Summary>
    private EnemyManager EnemyManager;
    private WaveManager WaveManager;

    /// <summary> Grid Cells, set in inspector, used to place paths based on the path grid</summary>
    public GridCellScriptableObject[] pathCellObjects;
    /// <summary> Grid Cells, set in inspector, fills empty space that is not used by the path grid </summary>
    public GridCellScriptableObject[] sceneryCellObjects;

    private PathGenerator pathGenerator;

    void Start()
    {
        pathGenerator = new PathGenerator(gridWidth, gridHeight);
        WaveManager = GetComponent<WaveManager>();
        
        Generate();
    }

    /// <summary>
    /// Starts the generation and building of the path
    /// </summary>
    /// <param name="check"></param>
    private void Generate()
    {
        List<Vector2Int> pathCells = pathGenerator.GeneratePath();
        int pathSize = pathCells.Count;
        while (pathGenerator.GenerateCrossroads())
            pathSize = pathCells.Count;

        while (pathSize < minPathLength)
        {
            pathCells = pathGenerator.GeneratePath();
            while (pathGenerator.GenerateCrossroads())
                pathSize = pathCells.Count;
        }

        StartCoroutine(CreateGrid(pathCells));
    }

    private IEnumerator CreateGrid(List<Vector2Int> pathCells)
    {
        yield return LayPathCells(pathCells);
        yield return LaySceneryCells();

        //EnemyManager.SetPathCell(pathGenerator.GenerateRoute());
        List<Vector2Int> cellPoints = pathGenerator.GenerateRoute();

        yield return new WaitForSeconds(2f);
        List<Vector3> path = new List<Vector3>();
        foreach (Vector2Int point in cellPoints)
        {
            path.Add(new Vector3(point.x, 1f, point.y));
        }
        WaveManager.EnemySpawner.Path = path;
        WaveManager.enabled = true;
    }

    private IEnumerator LayPathCells(List<Vector2Int> pathCells)
    {
        foreach(Vector2Int pathCell in pathCells)
        {
            int neighborValue = pathGenerator.GetCellNeighborValue(pathCell.x, pathCell.y);

            GameObject pathTile = pathCellObjects[neighborValue].cellPrefab;

            GameObject pathTileCell = Instantiate(pathTile, new Vector3(pathCell.x, 0f, pathCell.y), Quaternion.identity);
            pathTileCell.transform.parent = transform;
            pathTileCell.transform.Rotate(0f, pathCellObjects[neighborValue].yRotation, 0f);
            yield return null;
        }
        yield return null;
    }

    private IEnumerator LaySceneryCells()
    {
        for(int x = 0; x < gridWidth; x++)
        {
            for(int y = 0; y < gridHeight; y++)
            {
                if(pathGenerator.CellIsEmpty(x, y))
                {
                    int randomCellIndex = Random.Range(0, sceneryCellObjects.Length);
                    GameObject sceneryTileCell = Instantiate(sceneryCellObjects[randomCellIndex].cellPrefab, new Vector3(x, 0f, y), Quaternion.identity);
                    sceneryTileCell.transform.parent = transform;
                    yield return null;
                }
            }
        }
    }

}
