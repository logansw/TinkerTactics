using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MapLocation : MonoBehaviour
{
    public List<MapLocation> NextLocations;
    protected Button _button;
    private bool _initialized;
    
    public void Awake()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        if (_initialized) { return; }
        _button = GetComponent<Button>();
        _button.interactable = false;
        _button.onClick.AddListener(SetAsCurrentLocation);
        _initialized = true;
    }

    public void SetActive(bool active)
    {
        _button.interactable = active;
    }

    private void SetAsCurrentLocation()
    {
        MapManager.s_Instance.SetLocation(this);
        MapManager.s_Instance.OpenNextLocations();
    }
}