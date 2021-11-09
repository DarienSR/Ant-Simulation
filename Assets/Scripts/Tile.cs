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
    private SpriteRenderer originalSpriteR;
    float decreaseColorValue = 10f;
    float increaseColorValue = 2f;

    public string tileID;
    public GameObject[] neighbours;
    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        originalSpriteR = gameObject.GetComponent<SpriteRenderer>();
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

            // map.tileMap[x-1, y-1],
            // map.tileMap[x, y-1],
            // map.tileMap[x+1, y-1]
        };

    }

    public GameObject SelectNeighbour(int index) {
        GameObject[] neighbours = getNeighbours();
        GameObject selected = neighbours[index];
        if(selected.name == "Border(Clone)") selected = map.tileMap[x, y];
        return selected;
    }   

    private void FadeColor()
    {
        if(spriteR.color.g < 194)
            spriteR.color = new Color(spriteR.color.r, spriteR.color.g + 0.1f * Time.deltaTime, spriteR.color.b + 0.1f * Time.deltaTime);
    }

    public void AddColor()
    {
        spriteR.color = new Color(spriteR.color.r, spriteR.color.g - 2f * Time.deltaTime, spriteR.color.b -2f * Time.deltaTime);
    }
}
