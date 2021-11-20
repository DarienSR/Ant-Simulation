using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
  int health = 1;
  // When a gatherer collides with the food source, delete the food source and update the ant's state
  private void OnTriggerEnter2D(Collider2D gatherer) 
  {
    health--;
    Gatherer gathererObj = gatherer.gameObject.GetComponent<Gatherer>();
    if(health <= 0)
    {
      Destroy(gameObject); // destory food source
    }

    gathererObj.grid.tileMap[gathererObj.x, gathererObj.y].GetComponent<Tile>().hasFood = false; // update the tile to reflect it no longer containing food
    gathererObj.UpdateState(Gatherer.State.SUCCESS, true); 
    gathererObj.SetIndex();
  }
}

