using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Tile : MonoBehaviour
{
    public GameObject grid;
    public GameObject UI;
    private GridMap map;
    public int x;
    public int y;

    private SpriteRenderer spriteR;

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
        UI = GameObject.Find("UI");
        map = grid.GetComponent<GridMap>();
    }

    // Update is called once per frame
    void Update()
    {
        FadeColor();
    }

    private GameObject[] getNeighboursWithinMovement()
    {
        bool[] movementOptions = UI.GetComponent<UI>().GetControls();
        List<GameObject> movement = new List<GameObject>();
        if(movementOptions[0]) movement.Add(map.tileMap[x-1, y+1]);
        if(movementOptions[1]) movement.Add(map.tileMap[x, y+1]);    
        if(movementOptions[2]) movement.Add(map.tileMap[x+1, y+1]);
        if(movementOptions[3]) movement.Add(map.tileMap[x-1, y]);  
        if(movementOptions[4]) movement.Add(map.tileMap[x+1, y]);
        if(movementOptions[5]) movement.Add(map.tileMap[x-1, y-1]);   
        if(movementOptions[6]) movement.Add(map.tileMap[x, y-1]);
        if(movementOptions[7]) movement.Add(map.tileMap[x+1, y-1]);  

        return movement.ToArray();

    }


    public GameObject SelectNeighbour(int index) 
    {
        GameObject[] neighbours = getNeighboursWithinMovement();
        if(neighbours.Length == 0) return map.tileMap[x, y];
        GameObject selected = neighbours[index];
        if(selected.name == "Border(Clone)") selected = null;
        return selected;
    }   

    private GameObject[] getAllNeighbours(int radius)
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
		GameObject[] neighbours = getAllNeighbours(radius);
        int i = 0;
        List<GameObject> withFood = new List<GameObject>();
		foreach (GameObject tile in neighbours)
		{
			if(tile.GetComponent<Tile>().hasFood == true) withFood.Add(tile);
		}

        if(withFood.Count != 0)
        {
            int select = UnityEngine.Random.Range(0, withFood.Count);
            return withFood.ToArray()[select];
        }
		return null;
    }

    private void FadeColor()
    {
        if(spriteR.color.g < 194)
            spriteR.color = new Color(spriteR.color.r + 0.03f, spriteR.color.g + 0.03f, spriteR.color.b + 0.03f);
    }

    public void AddColor()
    {
        spriteR.color = new Color(spriteR.color.r * 0.05f, (spriteR.color.g * 0.05f), spriteR.color.b  * 0.05f);
    }

    public void UpdatePheromone()
    {
        pheromone += (0.9f * Time.deltaTime) + 1;
    }
}
