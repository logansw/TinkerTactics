using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// High-level component for a tower. Contains the core logic for attacking enemies.
/// </summary>
public abstract class Tower : MonoBehaviour, ISelectable, ILiftable
{
    private TilePlot _tilePlot;
    private TilePlot TilePlot
    {
        get
        {
            return _tilePlot;
        }
        set
        {
            _tilePlot = value;
            Active = _tilePlot.IsActivated;
        }
    }
    public string Name;
    [HideInInspector] public RangeIndicator RangeIndicator;
    public List<IAbility> Abilities;
    public float Range;
    public float Sweep;
    public TowerHealth Health;
    public bool Active;


    public abstract string GetTooltipText();

    protected virtual void Awake()
    {
        Abilities = gameObject.GetComponents<IAbility>().ToList();
        foreach (IAbility ability in Abilities)
        {
            ability.Initialize();
        }
        RangeIndicator = GetComponentInChildren<RangeIndicator>();
        RangeIndicator.Initialize(this);
    }

    void Start()
    {
        AssignTowerToTilePlot();
    }

    protected virtual void Update()
    {
        if (!Active)
        {
            return;
        }
        foreach (IAbility ability in Abilities)
        {
            if (ability.IsReloaded())
            {
                ability.Activate();
            }
            ability.InternalClock += Time.deltaTime;
        }
    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {

    }

    public void OnSelect()
    {
    }

    public void OnDeselect()
    {
    }

    /// <summary>
    /// Assigns the tower to a tile plot by performing a raycast from the tower's position.
    /// If an empty tile plot is found, the tower is assigned to it. 
    /// If an occupied tile plot is found, the towers are swapped.
    /// If no tile plot is found, the tower is returned to its previous position.
    /// </summary>
    public void AssignTowerToTilePlot()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.GetComponent<TilePlot>() != null)
            {
                TilePlot otherPlot = hit.collider.GetComponent<TilePlot>();
                if (otherPlot.AddTower(this))
                {
                    TilePlot = otherPlot;
                    return;
                }
                else
                {
                    // Swap Towers
                    Tower otherTower = otherPlot.Towers[0];
                    otherPlot.RemoveTower(otherTower);
                    otherPlot.AddTower(this);
                    TilePlot.RemoveTower(this);
                    TilePlot.AddTower(otherTower);
                    TilePlot = otherPlot;
                }
            }
        }
        // Return to plot if no tile plot is found
        ReturnToPlot();
    }

    private void ReturnToPlot()
    {
        transform.position = TilePlot.transform.position;
    }

    public void OnLift()
    {
        if (TilePlot != null)
        {
            TilePlot.RemoveTower(this);
        }
    }

    public void OnDrop()
    {
        AssignTowerToTilePlot();
    }

    public void OnHover()
    {

    }

    public void OnHeld()
    {
        // Do nothing
    }
}