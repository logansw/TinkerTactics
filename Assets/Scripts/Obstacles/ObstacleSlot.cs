using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ObstacleSlot : MonoBehaviour
{
    public Obstacle Obstacle { get; set; }
    public bool IsPurchased { get; set; } = true;
    public bool IsActivated { get; set; } = false;
    public bool IsOccupied => Obstacle != null;

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