using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    int health = 1;

    void OnTriggerEnter2D(Collider2D ant)
    {
        ant.gameObject.GetComponent<Ant>().UpdateAntToScavenger();
        health--;
        if(health <= 0)
            Destroy(gameObject);
    }
}
