using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    public GridMap grid;
    public int x;
    public int y;
    private int destroyTime;
    public GameObject trailPrefab; 
    

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<GridMap>();
        destroyTime = 2;
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
        GameObject trail = (GameObject) Instantiate(trailPrefab);
        trail.transform.position = currentTile.transform.position;
        StartCoroutine(DestroyTrail(trail));
    }

    IEnumerator DestroyTrail(GameObject trail)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(trail);
    }
    

    public GameObject Move() 
    {
        // Get Current Grid Tile

        GameObject currentTileGO = (GameObject)grid.tileMap[x, y];
        
        Tile currentTile = currentTileGO.GetComponent<Tile>();
        // Determine where to move
        int selectedTileIndex = Random.Range(0, 9);

        // Get that tiles position
        GameObject selectedTile = currentTile.SelectNeighbour(selectedTileIndex);
        x = (int)selectedTile.transform.position.x;
        y = (int)selectedTile.transform.position.y;
        Vector2 newPos = new Vector2(selectedTile.transform.position.x, selectedTile.transform.position.y);
        // Set position to that tile
        gameObject.transform.position = newPos;
        return selectedTile; // will be used to update the pheromone level of the tile
    }
}
