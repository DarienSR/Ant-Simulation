using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
  int health = 1;
  private void OnTriggerEnter2D(Collider2D gatherer) 
  {
    health--;
    Gatherer gathererObj = gatherer.gameObject.GetComponent<Gatherer>();
    if(health <= 0)
    {
      Destroy(gameObject); // destory food source
    }

    gathererObj.grid.tileMap[gathererObj.x, gathererObj.y].GetComponent<Tile>().hasFood = false;
    gathererObj.UpdateState(Gatherer.State.SUCCESS, true); 
    gathererObj.SetIndex();
  }
}

