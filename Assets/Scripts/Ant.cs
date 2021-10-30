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
        if(antType == "Scout")
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
        // TO DO: IF HEAD BACK TO NEST (AND NOT SCOUT): GO TO TILE WITH LEAST AMOUNT OF PHEROMONE (THAT ISNT 0)
        // TO DO: IF HEADING FOR FOOD (AND NOT SCOUT): GO TO TILE WITH HIGHEST PHEROMONE LEVEL

        if(antType == "Scout")
        {
            SelectRandomTile();
        } 
        else
        {
            if(headToNest)
            {
                // work backwards from the path.
                GameObject lastVisitedTile = path[path.Count - 1];
                lastVisitedTile.GetComponent<Tile>().UpdatePheromoneLevel(pheromoneStrength - 1); // only add pheromone once per ant, per tile
                MoveAnt(lastVisitedTile);
                path.RemoveAt(path.Count - 1);
             
            } else
            {
               Debug.Log("ANOTHER TRIP");

            }
        }

    }



    private void SelectRandomTile()
    {
        // Get Current Grid Tile
        GameObject currentTileGO = (GameObject)grid.tileMap[x, y];
        
        Tile currentTile = currentTileGO.GetComponent<Tile>();
        // Determine where to move
        int selectedTileIndex = Random.Range(0, 9);

        // Get that tiles position
        GameObject selectedTile = currentTile.SelectNeighbour(selectedTileIndex);
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
        // Update antType
        antType = "Scavenger";
        // Update trail color
        trailColor = Color.blue;
        // update pheromone strength
        headToNest = true;
    }

    public void UpdateAntToGatherer()
    {
        // Update antType
        antType = "Gatherer";
        // Update trail color
        trailColor = Color.yellow;
        headToNest = false;
        pheromoneStrength = path.Count;
    }
}
