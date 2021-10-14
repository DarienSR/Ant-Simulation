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
    public GameObject nestOpening;
    public GameObject nestBorder;

    public GameObject antPrefab;


    public GameObject[,] tileMap;
    private GameObject[] ants;
    
    private int nestWidth = 6;

    private int biomeSeperatorAtY;
    // Start is called before the first frame update
    void Start()
    {
        tileMap = new GameObject[rows, cols];
        ants = new GameObject[antStartSize];
        biomeSeperatorAtY = 40;
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
                // Destory orignal dirt/grass blocks
                Destroy(tileMap[biomeSeperatorAtY + j, (rows / 2) + i - j]);
                Destroy(tileMap[biomeSeperatorAtY + j, (rows / 2) - i + j]);
                Destroy(tileMap[biomeSeperatorAtY + j, (rows / 2)]);

                // nest border and area ([up/down, across])
                // right side of nest
                Transform posBorderRight = tileMap[biomeSeperatorAtY + j, (rows / 2) + i - j].transform;
                GameObject tileBorderRight = (GameObject)Instantiate(nestBorder, transform);
                tileBorderRight.transform.position = new Vector2(posBorderRight.position.x, posBorderRight.position.y);
                tileMap[biomeSeperatorAtY + j, (rows / 2) + i - j] = tileBorderRight;
                tileBorderRight.name = "TileBorder";

                // left side of nest
                Transform posBorderLeft = tileMap[biomeSeperatorAtY + j, (rows / 2) - i + j].transform;
                GameObject tileBorderLeft = (GameObject)Instantiate(nestBorder, transform);
                tileBorderLeft.transform.position = new Vector2(posBorderLeft.position.x, posBorderLeft.position.y);
                tileMap[biomeSeperatorAtY + j, (rows / 2) - i + j] = tileBorderLeft;
                tileBorderLeft.name = "TileBorder";
                
                // nest opening
                Transform posTileOpening = tileMap[biomeSeperatorAtY + j, (rows / 2)].transform;
                GameObject tileOpening = (GameObject)Instantiate(nestOpening, transform);
                tileOpening.transform.position = new Vector2(posTileOpening.position.x, posTileOpening.position.y);
                tileMap[biomeSeperatorAtY + j, (rows / 2)] = tileOpening;
                tileOpening.name = "TileOpening";
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
            ant.name = "Ant: " + i.ToString();
            ants[i] = ant;
        }
    }

    public GameObject[,] GetArray()
    {
        Debug.Log("HERE");
        return tileMap;
    }
}
