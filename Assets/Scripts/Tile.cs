using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
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
            map.tileMap[x+1, y],
            map.tileMap[x-1, y],
             //map.tileMap[x-1, y-1],
            // map.tileMap[x, y-1],
            //map.tileMap[x+1, y-1]
        };

    }


    public GameObject SelectNeighbour(int index) {
        GameObject[] neighbours = getNeighbours();
        GameObject selected = neighbours[index];
        if(selected.name == "Border(Clone)") selected = null;
        return selected;
    }   

    private GameObject[] getNeighboursWithFood(int radius)
    {
        neighbours = new GameObject[8*radius];
        List<GameObject> list = new List<GameObject>();
        for(int i = 0; i < radius; i++)
        {
            try
            {
                list.Add(map.tileMap[x-i, y+i]);
                list.Add(map.tileMap[x, y+i]);
                list.Add(map.tileMap[x+i, y+i]);
                list.Add(map.tileMap[x-i, y]);
                list.Add(map.tileMap[x+i, y]);
                list.Add(map.tileMap[x-i, y-i]);
                list.Add(map.tileMap[x, y-i]);
                list.Add(map.tileMap[x+i, y-i]);
            } 
            catch(ArgumentOutOfRangeException e)
            {

            }
        }
        neighbours = list.ToArray();
        return neighbours;
    }

    public GameObject CheckIfNeighboursHaveFood(int radius)
    {
		GameObject[] neighbours = getNeighboursWithFood(radius);
		foreach (GameObject tile in neighbours)
		{
			if(tile.GetComponent<Tile>().hasFood == true) return tile;
		}
		return null;
    }

    private void FadeColor()
    {
        if(spriteR.color.g < 194)
            spriteR.color = new Color(spriteR.color.r, spriteR.color.g + 0.03f, spriteR.color.b + 0.03f);
    }

    public void AddColor()
    {
        spriteR.color = new Color(spriteR.color.r, (spriteR.color.g * 0.09f), spriteR.color.b  * 0.09f);
    }

    public void UpdatePheromone()
    {
        pheromone += (0.9f * Time.deltaTime) + 1;
    }
}
