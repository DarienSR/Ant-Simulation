using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    public enum State 
    {
        SEARCHING, // Ant is searching for a food source. 
        SUCCESS, // Ant has found food and is either returning back to the nest, or is coming from the nest in search of more food.
        FAIL, // Ant has failed iuts initial search.
        FOLLOW_SUCCESS, // After an ant has failed its inital search of food and is now following the path of a successful ant (if there are any avaiable)
    }

    private int failCap = 175;

    // This boolean value is used in conjunction with the SUCCESS state.
    bool hasFood = false; // indicates whether or not the ant is carrying food. If it is (true), it is heading back to the nest, if it is not (false) then it is coming from the nest after a sucessful trip. 

    public State state; // hold a reference to the current state of the ant 
    public GridMap grid; // hold a reference to the grid so we can access tile information 
   
    Color antColor; // hold the color of the ant, representing its current state.
    public int x; // ants x position on the grid 
    public int y; // ants y position on the grid

    public int index = -1; // path index. Contains the current tile the ant is on

    public List<GameObject> path = new List<GameObject>(); // holds all the path information (tiles)
    public List<string> pathid = new List<string>(); 


    // Set all the default values of the ant.
    void Start()
    {
        state = State.SEARCHING; // Set starting state of SEARCHING
        AssignAntColor(state); // assign ant color based on the state
        grid = GameObject.Find("Grid").GetComponent<GridMap>(); // reference the grid object
    }

    void Update()
    {
        Move(); 
    }

    // Assign the color of the ant based its current state.
    private void AssignAntColor(State state)
    {
        if(state == State.SEARCHING)
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        else if(state == State.SUCCESS)
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        else if(state == State.FOLLOW_SUCCESS)
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        else if(state == State.FAIL)
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    // Movement logic based on ant state
    private void Move()
    {   
        CheckIfFailed();

        // When searching choose a random tile and add it to the path. 
        if(state == State.SEARCHING)
        {
            RandomWalk();
        }
        else if(state == State.SUCCESS) // ant has found food
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
        else if(state == State.FAIL)
        {
            // head back to the nest
            if(index != 0)
            {
                HeadBackToNest();
            }
            else
            {
                // reached nest.
                path.Clear();
                index = -1;
                UpdateState(State.FOLLOW_SUCCESS, false);
            }
        }
        else if(state == State.FOLLOW_SUCCESS)
        {
            Debug.Log("Start from fresh. Follow trails.");
        }
    }

    private void CheckIfFailed()
    {
        if(path.Count >= failCap) UpdateState(State.FAIL, false);
    }

    private void RandomWalk()
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
    
    // Should look to see if tiles contain food source, if so move to it. if not, perform a random walk.
    private void SearchForNeighbouringFood()
    {
        // Get Current Grid Tile
        GameObject currentTileGO = (GameObject)grid.tileMap[x, y];
        Tile currentTile = currentTileGO.GetComponent<Tile>();

        GameObject selectedTile = currentTile.CheckIfNeighboursHaveFood();
        if(selectedTile != null) // if we find a neighbouring tile with food
        {
            // move to selected tile
            MoveAnt(selectedTile);
            return;
        }
        // none of the neighbouring tiles have food. So just walk randomly until you find some
        UpdateState(State.SEARCHING, false);
        RandomWalk();
    }

    private void HeadBackToNest()
    {
        GameObject lastVistedTile = path[index]; 
        MoveAnt(lastVistedTile);
        if(state == State.SUCCESS) lastVistedTile.GetComponent<Tile>().UpdatePheromone();
        index--;
    }

    private void HeadBackToFoodSource()
    {
        index++;
        GameObject lastVistedTile = path[index]; 
        MoveAnt(lastVistedTile);
    }

    public void SetIndex()
    {
        index = path.Count - 1;
    }


    // Add tile that gameObject just moved to, to the path.
    private void AddToPath(GameObject tile)
    {
        path.Add(tile);
    }

    // update ant's positioning to the selectedTile
    private void MoveAnt(GameObject selectedTile)
    {
        x = (int)selectedTile.transform.position.x;
        y = (int)selectedTile.transform.position.y;
        Vector2 newPos = new Vector2(selectedTile.transform.position.x, selectedTile.transform.position.y);
        // Set position to that tile
        gameObject.transform.position = newPos;
        selectedTile.GetComponent<Tile>().AddColor();
        if(state == State.SEARCHING || state == State.FOLLOW_SUCCESS)
        {
            AddToPath(selectedTile);
        }
    }

    public void UpdateState(State newState, bool carryingFood)
    {
        state = newState;
        hasFood = carryingFood;
        AssignAntColor(state);
    }
}
