using UnityEngine;

[System.Serializable]
public class MapVariables
{
    public string Seed;
    public int GridWidth, GridHeight, MinPathLength, MaxPathLength;

    public MapVariables(string seed, int gridWidth, int gridHeight, int minPathLength, int maxPathLength)
    {
        Seed = seed;
        GridWidth = gridWidth;
        GridHeight = gridHeight;
        MinPathLength = minPathLength;
        MaxPathLength = maxPathLength;
    }
}
