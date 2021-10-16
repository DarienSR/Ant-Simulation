using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Ant : MonoBehaviour
{
  public float moveSpeed = 5f; 
  public Transform movePoint;
  public LayerMask Collider;
  string[] directions = new string[] {"Up", "Down", "Left", "Right"};

  void Start()
  {
    movePoint.parent = null;
  }
  // Update is called once per frame
  void Update()
  {
    transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime); // Move to new position
    string direction = directions[Random.Range(0, directions.Length)]; // select direction
    if(Vector3.Distance(transform.position, movePoint.position) < .01f) // when we have reached the new position
    {
      MoveAnt(direction);
    }  
  }

  void MoveAnt(string direction) 
  {
    if(direction == "Up")
    {
      if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 1f, 0f), .2f, Collider)) // do not move onto layers with collide
        movePoint.position += new Vector3(0f, 1f, 0f);
    } 
    else if(direction == "Down") 
    {
      if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f, 0f), .2f, Collider)) // do not move onto layers with collide
        movePoint.position += new Vector3(0f, -1f, 0f);
    }
    else if(direction == "Left") 
    {
      if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(-1f, 0f, 0f), .2f, Collider)) // do not move onto layers with collide
        movePoint.position += new Vector3(-1f, 0, 0f);
    }
    else if(direction == "Right") 
    {
      if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(1f, 0f, 0f), .2f, Collider)) // do not move onto layers with collide
        movePoint.position += new Vector3(1f, 0, 0f);
    }
  }
}
