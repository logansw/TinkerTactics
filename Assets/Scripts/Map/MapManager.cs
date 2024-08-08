using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

/// <summary>
/// Controls the manipulation of the map, including its initialization.
/// Acts as the outgoing interface for other scripts to interact with the map.
/// </summary>
[RequireComponent(typeof(MapModel)), RequireComponent(typeof(MapView))]
public class MapManager : Singleton<MapManager>
{
    /*
        0- Empty Space
        S- Start
        P- Path
        E- End
        T- Tile Plot
    */
    private string[] _mapData = new string[] {
        @"
        T"
    };
    private MapModel _mapModel;
    private MapView _mapView;
    public delegate void OnMapUpdated(Tile[,] tiles);
    public static event OnMapUpdated e_OnMapUpdated;

    protected override void Awake()
    {
        base.Awake();
        _mapModel = GetComponent<MapModel>();
        _mapView = GetComponent<MapView>();
    }

    void Start()
    {
        Tile[,] tiles = _mapView.SpawnTiles(CleanMapData(_mapData[0]));
        _mapModel.LoadMap(tiles);
        e_OnMapUpdated?.Invoke(tiles);
    }

    /// <summary>
    /// Helper method that removes white space from the map data string.
    /// </summary>
    private string CleanMapData(string mapData)
    {
        mapData = mapData[1..];
        return mapData.Replace(" ", "");
    }
}