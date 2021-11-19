using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class UI : MonoBehaviour
{
    public bool[] controls = new bool[] {true, true, true, true, true, false, false, false};
    public GameObject UP_LEFT;
    public GameObject UP;
    public GameObject UP_RIGHT;
    public GameObject LEFT;
    public GameObject RIGHT;
    public GameObject DOWN_LEFT;
    public GameObject DOWN;
    public GameObject DOWN_RIGHT;

    public GameObject slider;

    // Start is called before the first frame update
    void Start()
    {
        UP_LEFT.GetComponent<Image>().color = Color.red;
        UP.GetComponent<Image>().color = Color.red;
        UP_RIGHT.GetComponent<Image>().color = Color.red;
        LEFT.GetComponent<Image>().color = Color.red;
        RIGHT.GetComponent<Image>().color = Color.red;
    }

    public void UP_LEFT_CLICK()
    {
        UP_LEFT.GetComponent<Image>().color = UP_LEFT.GetComponent<Image>().color == Color.red ? Color.white : Color.red;
        controls[0] = !controls[0];
    }

    public void UP_CLICK()
    {
        UP.GetComponent<Image>().color = UP.GetComponent<Image>().color == Color.red ? Color.white : Color.red;
        controls[1] = !controls[1];
    }

    public void UP_RIGHT_CLICK()
    {
        UP_RIGHT.GetComponent<Image>().color = UP_RIGHT.GetComponent<Image>().color == Color.red ? Color.white : Color.red;
        controls[2] = !controls[2];
    }

    public void LEFT_CLICK()
    {
        LEFT.GetComponent<Image>().color = LEFT.GetComponent<Image>().color == Color.red ? Color.white : Color.red;
        controls[3] = !controls[3];
    }

    public void RIGHT_CLICK()
    {
        RIGHT.GetComponent<Image>().color = RIGHT.GetComponent<Image>().color == Color.red ? Color.white : Color.red;
        controls[4] = !controls[4];
    }

    public void DOWN_LEFT_CLICK()
    {
        DOWN_LEFT.GetComponent<Image>().color = DOWN_LEFT.GetComponent<Image>().color == Color.red ? Color.white : Color.red;
        controls[5] = !controls[5];     
    }
    
    public void DOWN_CLICK()
    {
        DOWN.GetComponent<Image>().color = DOWN.GetComponent<Image>().color == Color.red ? Color.white : Color.red;
        controls[6] = !controls[6];
    }

    public void DOWN_RIGHT_CLICK()
    {
        DOWN_RIGHT.GetComponent<Image>().color = DOWN_RIGHT.GetComponent<Image>().color == Color.red ? Color.white : Color.red;
        controls[7] = !controls[7];
    }

    public bool[] GetControls()
    {
        return controls;
    }

    public int GetNumOfMovementOptions()
    {
        return controls.Where(val => val == true).Count();
    }
}
