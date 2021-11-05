using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D ant)
    {
        if(ant.gameObject.GetComponent<Ant>().antType != "Scout")
        {
            ant.gameObject.GetComponent<Ant>().UpdateAntToGatherer();
        }
    }
}
