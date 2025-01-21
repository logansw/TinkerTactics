using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TilePlot : Tile
{
    public List<Tower> Towers { get; set; }
    public bool IsActivated;
    public int Capacity;
    private bool _updateQueued;

    void Start()
    {
        Capacity = 1;
        Towers = new List<Tower>();
    }

    void OnEnable()
    {
        PlayingState.e_OnPlayingStateEnter += QueueLockUpdate;
        IdleState.e_OnIdleStateEnter += QueueLockUpdate;
    }

    void OnDisable()
    {
        PlayingState.e_OnPlayingStateEnter -= QueueLockUpdate;
        IdleState.e_OnIdleStateEnter -= QueueLockUpdate;
    }

    public void Update()
    {
        if (_updateQueued)
        {
            _updateQueued = false;
        }
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
            Vector2 tilePosition = transform.position;
            tower.transform.position = new Vector3(tilePosition.x, tilePosition.y, tower.transform.position.z);
            return true;
        }
    }

    public void RemoveTower(Tower tower)
    {
        Towers.Remove(tower);
    }

    public void QueueLockUpdate()
    {
        _updateQueued = true;
    }

    public static TilePlot GetClosest(GameObject sourceObject)
    {
        Transform transform = sourceObject.transform;
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(1f, 1f), 0f, Vector2.zero);
        TilePlot closestPlot = null;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.GetComponent<TilePlot>() != null)
            {
                TilePlot hitPlot = hit.collider.GetComponent<TilePlot>();
                if (closestPlot == null)
                {
                    closestPlot = hitPlot;
                }
                if ((closestPlot.transform.position - transform.position).magnitude > (hitPlot.transform.position - transform.position).magnitude)
                {
                    closestPlot = hitPlot;
                }
            }
        }
        return closestPlot;
    }
}