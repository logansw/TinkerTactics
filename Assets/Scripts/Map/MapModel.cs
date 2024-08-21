using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapModel : MonoBehaviour
{
    private Tile[,] _tiles; // (0,0) refers to the bottom left corner.

    public void LoadMap(Tile[,] tiles)
    {
        _tiles = tiles;
    }

    public void DeleteMap()
    {
        foreach (Tile tile in _tiles)
        {
            if (tile != null)
            {
                Destroy(tile.gameObject);
            }
        }
    }
}