using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    public GameObject dirtPrefab;
    public GameObject borderPrefab;
    public GameObject grassPrefab;
    public GameObject antPrefab;

    public int antSpawnSize = 1050;

    public GameObject[,] tileMap;
    int rows = 100;
    int cols = 100;
    int yDivider;
    // Start is called before the first frame update
    void Start()
    {
        tileMap = new GameObject[rows, cols];
        yDivider = rows / 3;
        GenerateGrid();
        SpawnAnts();
    }

    // Update is called once per frame
    void GenerateGrid()
    {
        for(int i = 0; i < rows; i++) {
            for(int j = 0; j < cols; j++) {
                GameObject tile = SelectTilePrefab(i, j);
                tile.transform.SetParent(GameObject.Find("Grid").transform); // organize all tiles under the grid GameObject
                tile.transform.position = new Vector2(i, j);
                Tile t = tile.GetComponent<Tile>();
                t.x = i;
                t.y = j;
                tileMap[i, j] = tile;
            }   
        }
    }

    private GameObject SelectTilePrefab(int i, int j) 
    {
        if(i == rows / 2 && j == yDivider+1) // nest opening
            return (GameObject)Instantiate(grassPrefab); 
        else if(i == 0 || j == 0 || i == cols - 1 || j == rows - 1) // border around grid
            return (GameObject)Instantiate(borderPrefab); 
        else if(j <= yDivider) // dirt below division point
            return (GameObject)Instantiate(dirtPrefab);
        else if(j <= yDivider + 1) // border between above/below ground
            return (GameObject)Instantiate(borderPrefab);
        else // default is grass
            return (GameObject)Instantiate(grassPrefab); 
    }

    void SpawnAnts()
    {
        for(int i = 0; i < antSpawnSize; i++)
        {
            GameObject antGO = (GameObject)Instantiate(antPrefab);
            antGO.transform.SetParent(GameObject.Find("Colony").transform); // organize all ants under the colony GameObject
            antGO.transform.position = new Vector2(50, 36);
            Ant ant = antGO.GetComponent<Ant>();
            ant.x = 50;
            ant.y = 36;
        }
    }

    void GenerateNest()
    {
    }

}
