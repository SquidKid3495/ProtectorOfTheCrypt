using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridWidth = 10;
    public int gridHeight = 8;
    public int minPathLength = 15;

    private EnemyManager enemyManager;


    public GridCellScriptableObject[] pathCellObjects;
    public GridCellScriptableObject[] sceneryCellObjects;

    private PathGenerator pathGenerator;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 targetPos = Camera.main.transform.position;
        targetPos = new Vector3(gridWidth/2, 3.5f, -5.5f);
        Camera.main.transform.position = targetPos;
        pathGenerator = new PathGenerator(gridWidth, gridHeight);
        enemyManager = GetComponent<EnemyManager>();

        List<Vector2Int> pathCells = pathGenerator.GeneratePath();
        int pathSize = pathCells.Count;
        while(pathGenerator.GenerateCrossroads())
        pathSize = pathCells.Count;

        while(pathSize < minPathLength)
        {
            pathCells = pathGenerator.GeneratePath();
            while(pathGenerator.GenerateCrossroads())
            pathSize = pathCells.Count;
        }
        
        

        StartCoroutine(CreateGrid(pathCells));
    }

    private IEnumerator CreateGrid(List<Vector2Int> pathCells)
    {
        yield return LayPathCells(pathCells);
        yield return LaySceneryCells();
        enemyManager.SetPathCell(pathGenerator.GenerateRoute());
        enemyManager.SpawnWave();
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
        for(int x = -8; x < gridWidth+8; x++)
        {
            for(int y = -8; y < gridHeight+8; y++)
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
