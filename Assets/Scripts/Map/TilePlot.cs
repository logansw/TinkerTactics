using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TilePlot : Tile
{
    public List<Tower> Towers { get; set; }
    public bool IsActivated;
    public int Capacity;
    public bool IsTargeted { get; private set; }
    [SerializeField] private GameObject _targetedIndicator;
    [SerializeField] private GameObject _lockedIndicator;
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

    public void SetTargeted(bool targeted)
    {
        IsTargeted = targeted;
        _targetedIndicator.gameObject.SetActive(targeted);
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
}