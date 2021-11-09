using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
  int health = 1;
  private void OnTriggerEnter2D(Collider2D ant) 
  {
    health--;
    Ant antObj = ant.gameObject.GetComponent<Ant>();
    if(health <= 0)
    {
      Destroy(gameObject); // destory food source
      antObj.grid.tileMap[antObj.x, antObj.y].GetComponent<Tile>().hasFood = false;
    }
    antObj.UpdateState(Ant.State.SUCCESS, true);
    antObj.SetIndex();
  }
}

