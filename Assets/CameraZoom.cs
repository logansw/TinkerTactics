using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Camera camera = Camera.main;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float zoomSpeed = 2f;
        float minZoom = 5f;
        float maxZoom = 15f;
        if (scroll != 0.0f)
        {
            float newSize = camera.orthographicSize - scroll * zoomSpeed;
            camera.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        }
    }
}
