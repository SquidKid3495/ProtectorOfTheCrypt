using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Seed : MonoBehaviour
{
    public bool pickRandomSeed = true;
    [SerializeField] private string GameSeed = "default";
    private int CurrentSeed = 0;

    public MapVariablesSO mapLevels;
    private GridManager gridManager;

    private void Awake() 
    {
        gridManager = gameObject.GetComponent<GridManager>();

        if(pickRandomSeed)
            GameSeed = CreateRandomSeed(16);
        else
        {
            GameSeed = LoadCurrentSeed();
        }
        InitializeRandom();
    }

    private void InitializeRandom()
    {
        CurrentSeed = GameSeed.GetHashCode(); // Creates an integer seed based on the GameSeed string
        Random.InitState(CurrentSeed); // Initializes Random to use the seed, this means results using Random will be reproducable via a seed
    }

    private string CreateRandomSeed(int length)
    {
        string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_!?";
        string generated_string = "";

        for(int i = 0; i < length; i++)
            generated_string += characters[Random.Range(0, length)];

        return generated_string;
    }

    public void SaveCurrentSeed()
    {
        // Get Map width, height, and min path length
        int w = gridManager.gridWidth;
        int h = gridManager.gridHeight;
        int min = gridManager.minPathLength;
        int max = gridManager.maxPathLength;

        // Create a new variable containing all the important values
        MapVariables newMap = new MapVariables(GameSeed, w, h, min, max);

        // Save those values to a scriptable object list that contains all the levels
        mapLevels.mapVariablesList.Add(newMap);
    }

    public string LoadCurrentSeed()
    {
        string mapSeed = "";
        mapSeed = mapLevels.mapVariablesList[0].Seed;
        return mapSeed;
    }
}
