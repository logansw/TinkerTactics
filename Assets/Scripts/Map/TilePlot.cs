using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TilePlot : Tile
{
    public Tower Tower { get; set; }
    public bool IsPurchased { get; set; } = true;
    public bool IsActivated { get; set; } = false;
    public bool IsOccupied => Tower != null;

    public void Initialize(bool isPurchased, bool isActivated)
    {
        IsPurchased = isPurchased;
        IsActivated = isActivated;
    }
}