using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TilePlot : Tile
{
    public Tower Tower { get; set; }
    public bool IsPurchased { get; set; }
    public bool IsActivated { get; set; }
    public bool IsOccupied => Tower != null;

    void Start()
    {
        IsPurchased = true;
        IsActivated = true;
    }
}