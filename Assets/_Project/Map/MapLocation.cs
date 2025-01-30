using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MapLocation : MonoBehaviour
{
    public List<MapLocation> NextLocations;
    private Button _button;

    public void Start()
    {
        _button = GetComponent<Button>();
        _button.interactable = false;
        _button.onClick.AddListener(SetAsCurrentLocation);
    }

    public void SetActive(bool active)
    {
        _button.interactable = active;
    }

    private void SetAsCurrentLocation()
    {
        MapManager.s_Instance.SetLocation(this);
    }
}