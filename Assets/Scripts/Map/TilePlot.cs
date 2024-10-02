using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TilePlot : Tile
{
    public List<Tower> Towers { get; set; }
    public bool IsActivated { get; set; } = false;
    public int Capacity;

    void Start()
    {
        Capacity = 1;
        Towers = new List<Tower>();
    }

    public bool IsOccupied()
    {
        return Towers.Count >= Capacity;
    }

    /// <summary>
    /// Adds a tower to the tile plot if it is not fully occupied.
    /// </summary>
    /// <param name="tower"></param>
    /// <returns></returns>
    public bool AddTower(Tower tower)
    {
        if (IsOccupied())
        {
            return false;
        }
        else
        {
            Towers.Add(tower);
            tower.transform.position = transform.position;
            return true;
        }
    }

    public void RemoveTower(Tower tower)
    {
        Towers.Remove(tower);
    }
}