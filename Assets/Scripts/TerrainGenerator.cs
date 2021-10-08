using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    private int rows = 100;
    private int cols = 100;
    public int antStartSize = 50;
    public GameObject dirt;
    public GameObject grass;
    public GameObject antPrefab;
    public Material NestOpening;
    public Material NestBorder;

    private GameObject[,] tileMap;
    private GameObject[] ants;
    
    private int nestWidth = 6;

    private int biomeSeperatorAtY;
    // Start is called before the first frame update
    void Start()
    {
        tileMap = new GameObject[rows, cols];
        ants = new GameObject[antStartSize];
        biomeSeperatorAtY = 35;
        GenerateTerrain();
        GenerateNest();
        SpawnInitialAnts();
    }

    private void GenerateTerrain()
    {
        for(int row = 0; row < rows; row++)
        {
            for(int col = 0; col < cols; col++)
            {
                GameObject tile;
                if(row < biomeSeperatorAtY)
                    tile = (GameObject)Instantiate(dirt, transform);
                else
                    tile = (GameObject)Instantiate(grass, transform);
                float posX = col;
                float  posY = row;
                tile.transform.position = new Vector2(posX, posY); // Set positioning of crop tile

                tile.name = $"{col},{row}"; // set name of crop tile, which is the position if it
               
                tileMap[row, col] = tile;
            }
        }
    }

    private void GenerateNest()
    {
        int layers = 0;
        for(int i = nestWidth; i > 0; i--)
        {
            for(int j = 0; j < 3; j++)
            {
                // nest border and area ([up/down, across])
                tileMap[biomeSeperatorAtY + j, (rows / 2) + i - j].GetComponent<Renderer>().material = NestBorder; // right side
                tileMap[biomeSeperatorAtY + j, (rows / 2) - i + j].GetComponent<Renderer>().material = NestBorder; // left side

                // nest opening
                tileMap[biomeSeperatorAtY + j, (rows / 2)].GetComponent<Renderer>().material = NestOpening;
            }
        }
    }

    private void SpawnInitialAnts()
    {
        for(int i = 0; i < antStartSize; i++)
        {
            GameObject ant;
            ant = (GameObject)Instantiate(antPrefab, transform);
            ant.transform.position = tileMap[biomeSeperatorAtY + 4, (rows / 2)].transform.position;
            ant.name = i.ToString();
            ants[i] = ant;
        }
    }
}
