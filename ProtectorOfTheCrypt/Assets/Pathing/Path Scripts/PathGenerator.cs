using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator
{
    private int width,height;
    private List<Vector2Int> pathCells;
    private List<Vector2Int> route;

    public PathGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public List<Vector2Int> GeneratePath()
    {
        pathCells = new List<Vector2Int>();

        int y = (int)(height / 2);
        int x = 0;
        /*

        for(int x = 0; x < width; x++)
        {
            pathCells.Add(new Vector2Int(x,y));
        }*/

        while (x < width)
        {
            pathCells.Add(new Vector2Int(x,y));

            bool validMove = false;

            while(!validMove)
            {
                int move = Random.Range(0, 3);

                if(move == 0 || x % 2 == 0 && CellIsEmpty(x+1, y) || x > (width - 2))
                {
                    x++;
                    validMove = true;
                }
                else if(move == 1 && CellIsEmpty(x, y+1) && y < (height - 3))
                {
                    y++;
                    validMove = true;
                }
                else if(move == 2 && CellIsEmpty(x, y-1) && y > 2)
                {
                    y--;
                    validMove = true;
                }
            }
        }

        return pathCells;
    }

    public bool GenerateCrossroads()
    {
        for(int i = 0; i < pathCells.Count; i++)
        {
            Vector2Int pathCell = pathCells[i];
            if(pathCell.x > 3 && pathCell.x < width - 4 && pathCell.y > 2 && pathCell.y < height - 3) 
            { 
                // This is checking in a large area around the cell in question, if all of the spaces are available, a loop can be added
                if(CellIsEmpty(pathCell.x, pathCell.y+3) && CellIsEmpty(pathCell.x+1, pathCell.y+3) && CellIsEmpty(pathCell.x+2, pathCell.y+3)
                && CellIsEmpty(pathCell.x-1, pathCell.y+2) && CellIsEmpty(pathCell.x, pathCell.y+2) && CellIsEmpty(pathCell.x+1, pathCell.y+2) && CellIsEmpty(pathCell.x+2, pathCell.y+2) && CellIsEmpty(pathCell.x+3, pathCell.y+2)
                && CellIsEmpty(pathCell.x-1, pathCell.y+1) && CellIsEmpty(pathCell.x, pathCell.y+1) && CellIsEmpty(pathCell.x+1, pathCell.y+1) && CellIsEmpty(pathCell.x+2, pathCell.y+1) && CellIsEmpty(pathCell.x+3, pathCell.y+1)
                && CellIsEmpty(pathCell.x+1, pathCell.y) && CellIsEmpty(pathCell.x+2, pathCell.y) && CellIsEmpty(pathCell.x+3, pathCell.y)
                && CellIsEmpty(pathCell.x+1, pathCell.y-1) && CellIsEmpty(pathCell.x+2, pathCell.y-1))
                {
                    pathCells.InsertRange(i + 1, new List<Vector2Int> { new Vector2Int(pathCell.x + 1, pathCell.y), new Vector2Int(pathCell.x + 2, pathCell.y), new Vector2Int(pathCell.x + 2, pathCell.y + 1), new Vector2Int(pathCell.x + 2, pathCell.y + 2), new Vector2Int(pathCell.x + 1, pathCell.y + 2), new Vector2Int(pathCell.x, pathCell.y + 2), new Vector2Int(pathCell.x, pathCell.y + 1)});
                    return true;
                }

                if(CellIsEmpty(pathCell.x, pathCell.y-3) && CellIsEmpty(pathCell.x+1, pathCell.y-3) && CellIsEmpty(pathCell.x+2, pathCell.y-3)
                && CellIsEmpty(pathCell.x-1, pathCell.y-2) && CellIsEmpty(pathCell.x, pathCell.y-2) && CellIsEmpty(pathCell.x+1, pathCell.y-2) && CellIsEmpty(pathCell.x+2, pathCell.y-2) && CellIsEmpty(pathCell.x+3, pathCell.y-2)
                && CellIsEmpty(pathCell.x-1, pathCell.y-1) && CellIsEmpty(pathCell.x, pathCell.y-1) && CellIsEmpty(pathCell.x+1, pathCell.y-1) && CellIsEmpty(pathCell.x+2, pathCell.y-1) && CellIsEmpty(pathCell.x+3, pathCell.y-1)
                && CellIsEmpty(pathCell.x+1, pathCell.y) && CellIsEmpty(pathCell.x+2, pathCell.y) && CellIsEmpty(pathCell.x+3, pathCell.y)
                && CellIsEmpty(pathCell.x+1, pathCell.y+1) && CellIsEmpty(pathCell.x+2, pathCell.y+1))
                {
                    pathCells.InsertRange(i + 1, new List<Vector2Int> { new Vector2Int(pathCell.x + 1, pathCell.y), new Vector2Int(pathCell.x + 2, pathCell.y), new Vector2Int(pathCell.x + 2, pathCell.y - 1), new Vector2Int(pathCell.x + 2, pathCell.y - 2), new Vector2Int(pathCell.x + 1, pathCell.y - 2), new Vector2Int(pathCell.x, pathCell.y - 2), new Vector2Int(pathCell.x, pathCell.y - 1)});
                    return true;
                }
            }
        }
        return false;
    }

    public List<Vector2Int> GenerateRoute()
    {
        Vector2Int direction = Vector2Int.right;
        route = new List<Vector2Int>();
        Vector2Int currentCell = pathCells[0];

        while(currentCell.x < width)
        {
            route.Add(new Vector2Int(currentCell.x, currentCell.y));

            if(CellIsTaken(currentCell + direction))
            {
                currentCell += direction;
            }
            else if(CellIsTaken(currentCell + Vector2Int.up) && direction != Vector2Int.down)
            {
                direction = Vector2Int.up;
                currentCell += direction;
            }
            else if(CellIsTaken(currentCell + Vector2Int.down) && direction != Vector2Int.up)
            {
                direction = Vector2Int.down;
                currentCell += direction;
            }
            else if(CellIsTaken(currentCell + Vector2Int.left) && direction != Vector2Int.right)
            {
                direction = Vector2Int.left;
                currentCell += direction;
            }
            else if(CellIsTaken(currentCell + Vector2Int.right) && direction != Vector2Int.left)
            {
                direction = Vector2Int.right;
                currentCell += direction;
            }
            else
            {
                // No where to go, return route
                return route;
            }
        }
        return route;

        
    }
    public bool CellIsEmpty(int x, int y)
    {
        return !pathCells.Contains(new Vector2Int(x, y));
    }

    public bool CellIsTaken(int x, int y)
    {
        return pathCells.Contains(new Vector2Int(x, y));
    }
    public bool CellIsTaken(Vector2Int cell)
    {
        return pathCells.Contains(cell);
    }

    public int GetCellNeighborValue(int x, int y)
    {
        int returnValue = 0;
        if(CellIsTaken(x, y-1)) returnValue += 1;
        if(CellIsTaken(x-1, y)) returnValue += 2; //       8
        if(CellIsTaken(x+1, y)) returnValue += 4; //     2 C 4
        if(CellIsTaken(x, y+1)) returnValue += 8; //       1
        return returnValue;
    }
}
