using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    public GridMap grid;
    public int x;
    public int y;
    private Trail trail;
    public string antType = "Scout";
    private Color trailColor = Color.red;
    private float pheromoneStrength = 1f;
    bool headToNest = true;
    public int index = -1;
    public List<GameObject> path = new List<GameObject>();
    public List<string> pathid = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<GridMap>();
        trail = GetComponent<Trail>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PlaceTrail();
    }

    private void PlaceTrail()
    {
        GameObject currentTileGO = (GameObject)grid.tileMap[x, y];
        Tile currentTile = currentTileGO.GetComponent<Tile>();
        if(antType == "Scout" )
        {
            if(!pathid.Contains(currentTile.tileID))
            {
                pathid.Add(currentTile.tileID);
                path.Add(currentTileGO);
            }
        }

        trail.PlaceTrail(antType, trailColor, currentTile);
    }    

    public void Move() 
    {
        if(antType == "Scout")
        {
            SelectRandomTile();
        } 
        else
        {
            if(headToNest)
            {
                // work backwards from the path.
                Debug.Log(path.Count + " go to nest");
                GameObject lastVisitedTile = path[index];
                lastVisitedTile.GetComponent<Tile>().UpdatePheromoneLevel(pheromoneStrength + path.Count); // only add pheromone once per ant, per tile
                MoveAnt(lastVisitedTile);
                index--;             
            } else
            { // have returned from food source to nest
                Debug.Log(path.Count + " search for food");
                if(index < path.Count - 1) 
                {
                    GameObject lastVisitedTile = path[index];
                    lastVisitedTile.GetComponent<Tile>().UpdatePheromoneLevel(pheromoneStrength + path.Count); // only add pheromone once per ant, per tile
                    MoveAnt(lastVisitedTile);
                        index++;
                } 
                else 
                {
                    Debug.Log("Looking for more food");
                    SelectRandomTile(); // previous food source is gone, find more.
                }
            }
        }

    }

    private void SelectRandomTile()
    {
        // Get Current Grid Tile
        GameObject currentTileGO = (GameObject)grid.tileMap[x, y];
        
        Tile currentTile = currentTileGO.GetComponent<Tile>();
        // Determine where to move
        int selectedTileIndex = Random.Range(0, 5);
        // Get that tiles position
        GameObject selectedTile = currentTile.SelectNeighbour(selectedTileIndex);
        if(selectedTile == currentTileGO) return; 
        MoveAnt(selectedTile);

    }

    private void MoveAnt(GameObject selectedTile)
    {
        x = (int)selectedTile.transform.position.x;
        y = (int)selectedTile.transform.position.y;
        Vector2 newPos = new Vector2(selectedTile.transform.position.x, selectedTile.transform.position.y);
        // Set position to that tile
        gameObject.transform.position = newPos;
    }

    public void UpdateAntToScavenger()
    {
        index = path.Count - 1;
        // Update antType
        antType = "Scavenger";
        // Update trail color
        trailColor = Color.blue;
        // update pheromone strength
        headToNest = true;
    }

    public void UpdateAntToGatherer()
    {
        index = 0;
        // Update antType
        antType = "Gatherer";
        // Update trail color
        trailColor = Color.yellow;
        headToNest = false;
    }
}
