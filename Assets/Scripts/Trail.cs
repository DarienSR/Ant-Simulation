using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    private int destroyTime = 2;
    public GameObject scoutTrailPrefab; 
    
    public void PlaceTrail(string antType, Color trailColor, Tile currentTile)
    {
        GameObject trail = (GameObject) Instantiate(scoutTrailPrefab);
        trail.transform.position = currentTile.transform.position;
        trail.transform.SetParent(GameObject.Find("Trails").transform); // organize all tiles under the grid GameObject
        trail.GetComponent<SpriteRenderer>().color = trailColor;
        StartCoroutine(DestroyTrail(trail));
    }

    private IEnumerator DestroyTrail(GameObject trail)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(trail);
    }
}
