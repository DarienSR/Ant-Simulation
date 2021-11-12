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

    private SpriteRenderer spriteR;

    float decreaseColorValue = 10f;
    float increaseColorValue = 2f;

    public float pheromone = 0f;

    public bool hasFood;

    public string tileID;
    public GameObject[] neighbours;
    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        tileID = x +","+y;
        grid = GameObject.Find("Grid");
        map = grid.GetComponent<GridMap>();
    }

    // Update is called once per frame
    void Update()
    {
        FadeColor();
        if(pheromone > 0)
        {
            pheromone -= 1f * Time.deltaTime;
            if(pheromone < 0) pheromone = 0;
        }
    }

    private GameObject[] getNeighbours()
    {
        return neighbours = new GameObject[] {
            map.tileMap[x-1, y+1],
            map.tileMap[x, y+1],
            map.tileMap[x+1, y+1],

            map.tileMap[x-1, y],
            // map.tileMap[x, y],
            map.tileMap[x+1, y],

            // map.tileMap[x-1, y-1],
            // map.tileMap[x, y-1],
            // map.tileMap[x+1, y-1]
        };

    }

    public GameObject SelectFailNeighbours(int index)
    {
        GameObject[] neighbours = new GameObject[] {
            map.tileMap[x-1, y],
            map.tileMap[x+1, y],
            map.tileMap[x-1, y-1],
            map.tileMap[x, y-1],
            map.tileMap[x+1, y-1]
        };
        GameObject selected = neighbours[index];
        if(selected.name == "Border(Clone)") selected = null;
        return selected;
    }

    public GameObject SelectNeighbour(int index) {
        GameObject[] neighbours = getNeighbours();
        GameObject selected = neighbours[index];
        if(selected.name == "Border(Clone)") selected = map.tileMap[x+1, y-1];
        return selected;
    }   

    public GameObject CheckIfNeighboursHaveFood()
    {
		GameObject[] neighbours = getNeighbours();
		foreach (GameObject tile in neighbours)
		{
			if(tile.GetComponent<Tile>().hasFood == true) return tile;
		}
		return null;
    }

    private void FadeColor()
    {
        if(spriteR.color.g < 194)
            spriteR.color = new Color(spriteR.color.r, spriteR.color.g + 0.2f * Time.deltaTime, spriteR.color.b + 0.2f * Time.deltaTime);
    }

    public void AddColor()
    {
        spriteR.color = new Color(spriteR.color.r, spriteR.color.g - 0.5f * Time.deltaTime, spriteR.color.b -0.5f * Time.deltaTime);
    }

    public void UpdatePheromone()
    {
        pheromone += 5f * Time.deltaTime;
    }
}
