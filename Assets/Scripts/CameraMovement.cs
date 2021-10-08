using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera camera;

    // Update is called once per frame
    void Update()
    {
        HandleCameraMovement();
        HandleCameraZoom();
    }

    private void HandleCameraMovement()
    {        
        if(Input.GetKey("w"))
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + 1, camera.transform.position.z);
        if(Input.GetKey("a"))
            camera.transform.position = new Vector3(camera.transform.position.x - 1, camera.transform.position.y, camera.transform.position.z);
        if(Input.GetKey("s"))
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y - 1, camera.transform.position.z);
        if(Input.GetKey("d"))
            camera.transform.position = new Vector3(camera.transform.position.x + 1, camera.transform.position.y, camera.transform.position.z);
    }

    private void HandleCameraZoom()
    {
        if (Input.mouseScrollDelta.y > 0f ) // forward
            camera.orthographicSize -= 1; // zoom in
        if(Input.mouseScrollDelta.y < 0f ) // backwards
            camera.orthographicSize += 1; // zoom out
    }
}
