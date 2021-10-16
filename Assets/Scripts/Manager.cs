using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject antPrefab;
    GameObject[] ants;
    int antColonySize = 1000;
    // Start is called before the first frame update
    void Start()
    {
        ants = new GameObject[antColonySize];
        GenerateAnts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateAnts()
    {
        for(int i = 0; i < antColonySize; i++)
        {
            GameObject ant;
            ant = (GameObject)Instantiate(antPrefab, transform);
            ant.transform.position = new Vector2(-5, -15f);
            ant.name = "Ant: " + i.ToString();
            ants[i] = ant;
        }
    }
}
