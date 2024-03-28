using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TilePlot : MonoBehaviour
{
    public Tower Tower { get; set; }
    public bool IsPurchased { get; set; } = true;
    public bool IsActivated { get; set; } = false;
    public bool IsOccupied => Tower != null;

    void Start()
    {
        Initialize(true, true);
    }

    public void Initialize(bool isPurchased, bool isActivated)
    {
        IsPurchased = isPurchased;
        IsActivated = isActivated;
    }
}