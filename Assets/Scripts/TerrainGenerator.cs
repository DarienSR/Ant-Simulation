using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    private int rows = 100;
    private int cols = 100;
    public GameObject dirt;
    public GameObject grass;

    private int biomeSeperatorAtY;
    // Start is called before the first frame update
    void Start()
    {
        biomeSeperatorAtY = 35;
        GenerateTerrain();
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
            }
        }
    }
}
