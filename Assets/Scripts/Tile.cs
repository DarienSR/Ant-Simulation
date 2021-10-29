using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject grid;
    private GridMap map;
    public int x;
    public int y;

    float pheromoneLevel = 0;

    GameObject[] neighbours = new GameObject[8];
    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("Grid");
        map = grid.GetComponent<GridMap>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public GameObject SelectNeighbour(int index) {
        GameObject selected = map.tileMap[x, y];
        if(index == 0) selected = map.tileMap[x-1, y+1];
        if(index == 1) selected = map.tileMap[x, y+1];
        if(index == 2) selected = map.tileMap[x+1, y+1];
        if(index == 3) selected = map.tileMap[x-1, y];
        if(index == 4) selected = map.tileMap[x+1, y];
        if(index == 5) selected = map.tileMap[x-1, y-1];
        if(index == 6) selected = map.tileMap[x, y-1];
        if(index == 7) selected = map.tileMap[x+1, y-1];

        if(selected.name == "Border(Clone)") selected = map.tileMap[x, y];
       
        return selected;
    }   

    private void OnMouseDown() {
        Debug.Log(map.tileMap[x,y]);
    }

    public void UpdatePheromoneLevel()
    {

    }
}
