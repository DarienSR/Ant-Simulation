using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherer : MonoBehaviour
{
    public enum State 
    {
        SEARCHING, // Gatherer is searching for a food source. 
        SUCCESS, // Gatherer has found food and is either returning back to the nest, or is coming from the nest in search of more food.
        FAIL, // Gatherer has failed iuts initial search.
        FOLLOW_SUCCESS, // After an gatherer has failed its inital search of food and is now following the path of a successful gatherer (if there are any avaiable)
    }

    private int failCap = 225;
    public int searchRadius = 2;
    // This boolean value is used in conjunction with the SUCCESS state.
    bool hasFood = false; // indicates whether or not the gatherer is carrying food. If it is (true), it is heading back to the nest, if it is not (false) then it is coming from the nest after a sucessful trip. 

    public State state; // hold a reference to the current state of the gatherer 
    public GridMap grid; // hold a reference to the grid so we can access tile information 
   
    Color gathererColor; // hold the color of the gatherer, representing its current state.
    public int x; // gatherers x position on the grid 
    public int y; // gatherers y position on the grid

    public int index = -1; // path index. Contains the current tile the gatherer is on

    public List<GameObject> path = new List<GameObject>(); // holds all the path information (tiles)

    private UI ui;

    // Set all the default values of the gatherer.
    void Start()
    {
        state = State.SEARCHING; // Set starting state of SEARCHING
        AssignGathererColor(state); // assign gatherer color based on the state
        grid = GameObject.Find("Grid").GetComponent<GridMap>(); // reference the grid object
        ui = GameObject.Find("UI").GetComponent<UI>();
    }

    void Update()
    {
        Move(); 
    }

    // Assign the color of the gatherer based its current state.
    private void AssignGathererColor(State state)
    {
        if(state == State.SEARCHING)
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        else if(state == State.SUCCESS)
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        else if(state == State.FOLLOW_SUCCESS)
            gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
        else if(state == State.FAIL)
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    // Movement logic based on gatherer state
    private void Move()
    {   
        CheckIfFailed();

        // When searching choose a random tile and add it to the path. 
        if(state == State.SEARCHING)
        {
            RandomWalk();
        }
        else if(state == State.SUCCESS || state == State.FOLLOW_SUCCESS) // gatherer has found food
        {
            if(index != 0 && hasFood) // if you have not reached the nest
            {
                HeadBackToNest();
            }
            else  // you have reached the nest
            {
                if(index == 0) hasFood = false;
                
                // Head back to your lastVisted tile where you found the food
                if(!hasFood && index < path.Count - 1)
                {
                    HeadBackToFoodSource();
                    return;
                }
                else 
                {
                    // Search for more food, once you have reached the foodsource
                    SearchForNeighbouringFood();
                }
            }
        }
        else if(state == State.FAIL) // gatherer has failed to find a food source
        {
            // head back to the nest
            if(index != 0)
            {
                HeadBackToNest();
            }
            else // gatherer has reached the nest
            {
                path.Clear(); // reset the failed path
                index = 0; 
                List<GameObject> temp = new List<GameObject>(); // create a temp path to assign to a successful one 
                foreach (Gatherer gatherer in grid.gatherers) // iterate through all the gatherers
                {
                    if(gatherer.state == State.SUCCESS) // find the path in all of the gatherers that has the lowest tile count (i.e. the shortest)
                    {
                        if(temp.Count == 0) temp = gatherer.path; 
                        if(gatherer.path.Count < temp.Count) temp = gatherer.path;
                    }
                }
                if(temp.Count != 0) // if we find a successful path
                { 
                    foreach(GameObject tile in temp)
                    {
                        path.Add(tile); // copy the shortest successful gatherers' path
                    }
                    UpdateState(State.FOLLOW_SUCCESS, false); // follow the successful path
                    return;
                }
                // no path find, just random walk again
                UpdateState(State.SEARCHING, false);
            }
        }
    }

    // A function to check if the gatherer has reached the maximum tile count, if so update their state to FAIL
    private void CheckIfFailed()
    {
        if(path.Count >= failCap) UpdateState(State.FAIL, false);
    }

    // Randomly select a neighbouring tile. Check if you have reached the border of the map.
    private void RandomWalk()
    {
        // Get Current Grid Tile
        GameObject currentTileGO = (GameObject)grid.tileMap[x, y];
        
        Tile currentTile = currentTileGO.GetComponent<Tile>();
        // Determine where to move
        int selectedTileIndex = Random.Range(0, ui.GetNumOfMovementOptions());
        // Get that tiles position
        GameObject selectedTile = currentTile.SelectNeighbour(selectedTileIndex);

        // Check to see if the selected tile is a border
        if(selectedTile == null)
        {
            selectedTile = currentTileGO;
            HeadBackToNest();
            UpdateState(State.FAIL, false);
        }
        if(selectedTile == currentTileGO) return; 
        MoveGatherer(selectedTile);
    }
    
    // Should look to see if tiles contain food source, if so move to it. if not, perform a random walk.
    private void SearchForNeighbouringFood()
    {
        // Get Current Grid Tile
        GameObject currentTileGO = (GameObject)grid.tileMap[x, y];
        Tile currentTile = currentTileGO.GetComponent<Tile>();

        GameObject selectedTile = currentTile.CheckIfNeighboursHaveFood(searchRadius);
        if(selectedTile != null) // if we find a neighbouring tile with food
        {
            // move to selected tile
            MoveGatherer(selectedTile);
            AddToPath(selectedTile);
            return;
        }
        // none of the neighbouring tiles have food. So just walk randomly until you find some
        UpdateState(State.SEARCHING, false);
        RandomWalk();
    }

    // A function that traces the path back to the nest. 
    private void HeadBackToNest()
    {
        index--;
        GameObject lastVistedTile = path[index]; 
        MoveGatherer(lastVistedTile);
        if(state == State.SUCCESS) lastVistedTile.GetComponent<Tile>().UpdatePheromone();
    }

    // A function that heads from the nest back to the food source it came from
    private void HeadBackToFoodSource()
    {
        index++;
        GameObject lastVistedTile = path[index]; 
        Tile tile = lastVistedTile.GetComponent<Tile>();
        
        GameObject cutPathShort = tile.CheckIfNeighboursHaveFood(searchRadius);
        // we have encountered food before reaching our previous end tile 
        if(cutPathShort != null) 
        {
            path.RemoveRange(index+1, path.Count - (index+1)); // remove the further away tiles.
            MoveGatherer(cutPathShort);
            AddToPath(cutPathShort);
        } 
        else
            MoveGatherer(lastVistedTile); // no optimization found
    }

    public void SetIndex()
    {
        index = path.Count - 1;
    }

    // Add tile that gameObject just moved to, to the path.
    private void AddToPath(GameObject tile)
    {
        path.Add(tile);
        index++;
    }
    // update gatherer's positioning to the selectedTile
    private void MoveGatherer(GameObject selectedTile)
    {
        x = (int)selectedTile.transform.position.x;
        y = (int)selectedTile.transform.position.y;
        Vector2 newPos = new Vector2(selectedTile.transform.position.x, selectedTile.transform.position.y);
        // Set position to that tile
        gameObject.transform.position = newPos;
        selectedTile.GetComponent<Tile>().AddColor();
        if(state == State.SEARCHING)
        {
            AddToPath(selectedTile);
        }
    }

    public void UpdateState(State newState, bool carryingFood)
    {
        state = newState;
        hasFood = carryingFood;
        AssignGathererColor(state);
    }
}
