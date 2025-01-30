using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    private CameraZoom _cameraZoom;
    private DragCamera _dragCamera;

    public void AddControls()
    {
        if (_cameraZoom == null)
        {
            _cameraZoom = gameObject.AddComponent<CameraZoom>();
        }
        else
        {
            _cameraZoom.enabled = true;
        }
        if (_dragCamera == null)    
        {
            _dragCamera = gameObject.AddComponent<DragCamera>();
        }
        else
        {
            _dragCamera.enabled = true;
        }
    }

    public void RemoveControls()
    {
        if (_cameraZoom != null)
        {
            _cameraZoom.enabled = false;
        }
        if (_dragCamera != null)
        {
            _dragCamera.enabled = false;
        }
    }
}
