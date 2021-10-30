using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tile : MonoBehaviour
{
    public GameObject grid;
    private GridMap map;
    public int x;
    public int y;

    public float pheromoneLevel = 0;
    public string tileID;
    public GameObject[] neighbours;
    // Start is called before the first frame update
    void Start()
    {
        tileID = x +","+y;
        grid = GameObject.Find("Grid");
        map = grid.GetComponent<GridMap>();
        neighbours = new GameObject[3] {
            map.tileMap[x-1, y+1],
            map.tileMap[x, y+1],
            map.tileMap[x+1, y+1],

            // map.tileMap[x-1, y],
            // map.tileMap[x, y],
            // map.tileMap[x+1, y],

            // map.tileMap[x-1, y-1],
            // map.tileMap[x, y-1],
            // map.tileMap[x+1, y-1]
        };

    }

    // Update is called once per frame
    void Update()
    {
        if(pheromoneLevel > 0)
            pheromoneLevel -= 0.05f * Time.deltaTime;
    }

    public GameObject SelectNeighbour(int index) {

        GameObject selected = neighbours[index];
        if(selected.name == "Border(Clone)") selected = map.tileMap[x, y];
        return selected;
    }   

    public GameObject SelectNeighbourSmallestPheromone()
    {
        float pheromone = pheromoneLevel;
        GameObject r = null;
        foreach (var item in neighbours)
        {
            if(item.GetComponent<Tile>().pheromoneLevel <= pheromone && item.GetComponent<Tile>().pheromoneLevel != 0)
            {
                pheromone = item.GetComponent<Tile>().pheromoneLevel;
                r = item;
            }
        }
        Debug.Log(r);
        return r;
    }

    public GameObject SelectNeighbourLargestPheromone()
    {
        float pheromone = pheromoneLevel;
        GameObject r = null;
        foreach (var item in neighbours)
        {
            if(item.GetComponent<Tile>().pheromoneLevel <= pheromone && item.GetComponent<Tile>().pheromoneLevel != 0)
            {
                pheromone = item.GetComponent<Tile>().pheromoneLevel;
                r = item;
            }
        }
        Debug.Log(r);
        return r;
    }

    public void UpdatePheromoneLevel(float pheromoneStrength)
    {
        pheromoneLevel += pheromoneStrength;
    }
}
