using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
  int health = 1;
  private void OnTriggerEnter2D(Collider2D ant) 
  {
    health--;
    if(health <= 0)
        Destroy(gameObject);
    ant.gameObject.GetComponent<Ant>().UpdateState(Ant.State.SUCCESS, true);
    ant.gameObject.GetComponent<Ant>().SetIndex();
  }
}

