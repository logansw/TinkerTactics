using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private MapLocation _startLocation;
    private MapLocation _currentLocation;
    // [SerializeField] private MapLocation _battlePrefab;
    // [SerializeField] private MapLocation _shopPrefab;
    // [SerializeField] private MapLocation _bonfirePrefab;
    private List<MapLocation> _siblingLocations;
    public bool Loaded { get; private set; }

    public override void Initialize()
    {
        base.Initialize();
        _startLocation.Initialize();
        _startLocation.SetActive(true);
        _currentLocation = _startLocation;
        _siblingLocations = new List<MapLocation>();
        Loaded = true;
    }

    public void OpenNextLocations()
    {
        _siblingLocations = new List<MapLocation>();
        foreach (MapLocation nextLocation in _currentLocation.NextLocations)
        {
            nextLocation.SetActive(true);
            _siblingLocations.Add(nextLocation);
        }
        _currentLocation.SetActive(false);
    }

    public void SetLocation(MapLocation mapLocation)
    {
        foreach (MapLocation siblingLocation in _siblingLocations)
        {
            siblingLocation.SetActive(false);
        }
        _currentLocation = mapLocation;
        _currentLocation.SetActive(true);
    }

    public void MinimizeMapScene()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void RestoreMapScene()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}