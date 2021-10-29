using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    int health = 5;

    // called when the cube hits the floor
    void OnTriggerEnter2D(Collider2D ant)
    {
        if(health <= 0)
            Destroy(gameObject);
        else
        {
            ant.gameObject.GetComponent<Ant>().BringFoodBackToNest();
            health--;
        }
    }
}
