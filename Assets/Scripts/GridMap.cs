using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GridMap : MonoBehaviour
{
    public GameObject dirtPrefab;
    public GameObject borderPrefab;
    public GameObject grassPrefab;
    public GameObject gathererPrefab;
    public GameObject foodPrefab;
    public GameObject nestPrefab;

    public int gathererSpawnSize = 1050;

    private Vector2 nestNode = new Vector2(50, 2);
    public GameObject[,] tileMap;
    public Gatherer[] gatherers;
    int rows = 100;
    int cols = 100;
    // Start is called before the first frame update
    void Start()
    {
        tileMap = new GameObject[rows, cols];
        gatherers = new Gatherer[gathererSpawnSize];
        GenerateGrid();
        SpawnGatherers();
        SpawnFood(3, 50);
       
        SpawnFood(50, 30);
        SpawnFood(70, 70);
        SpawnFood(10, 80);
        SpawnFood(10, 10);
        SpawnFood(80, 20);
        SpawnNestNode(nestNode);
    }

    void Update()
    {
        // reset scene/sim on spacebar click
        if(Input.GetKeyUp("space"))
        {
            SceneManager.LoadScene("SampleScene");
        }
        if(Input.GetKeyDown("escape")) Application.Quit();
    }
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
        if(i == 0 || j == 0 || i == cols - 1 || j == rows - 1) // border around grid
            return (GameObject)Instantiate(borderPrefab); 
        else // default is grass
            return (GameObject)Instantiate(grassPrefab); 
    }

    private void SpawnGatherers()
    {
        for(int i = 0; i < gathererSpawnSize; i++)
        {
            GameObject gathererGO = (GameObject)Instantiate(gathererPrefab);
            gathererGO.transform.SetParent(GameObject.Find("Colony").transform); // organize all gatherers under the colony GameObject
            gathererGO.transform.position = nestNode; // spawn gatherers on nest node
            Gatherer gatherer = gathererGO.GetComponent<Gatherer>();
            gatherer.x = (int)nestNode.x;
            gatherer.y = (int)nestNode.y;
            gatherers[i] = gatherer;
        }
    }

    private void SpawnFood(int x, int y)
    {
        for(int i = 0; i < 15; i++)
        {
            for(int j = 0; j < 15; j++)
            {
                GameObject foodGO = (GameObject)Instantiate(foodPrefab);
                foodGO.transform.SetParent(GameObject.Find("FoodSource").transform); // organize all gatherers under the colony GameObject
                foodGO.transform.position = new Vector2(x+i, y+j);
                tileMap[x+i, y+j].GetComponent<Tile>().hasFood = true;
            }
        }
    }

    private void SpawnNestNode(Vector2 pos)
    {
        GameObject nestNode = (GameObject)Instantiate(nestPrefab);
        nestNode.transform.SetParent(GameObject.Find("Nest").transform); // organize all gatherers under the colony GameObject
        nestNode.transform.position = pos;
    }
}
