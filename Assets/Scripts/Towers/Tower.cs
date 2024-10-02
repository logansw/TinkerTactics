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
    public string Name;
    [HideInInspector] public RangeIndicator RangeIndicator;
    public List<IAbility> Abilities;
    public float Range;
    public float Sweep;


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

    protected virtual void Update()
    {
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
                    _tilePlot = otherPlot;
                    return;
                }
                else
                {
                    ReturnToPlot();
                }
            }
        }
        // Return to plot if no tile plot is found
        ReturnToPlot();
    }

    private void ReturnToPlot()
    {
        transform.position = _tilePlot.transform.position;
    }

    public void OnLift()
    {
        if (_tilePlot != null)
        {
            _tilePlot.RemoveTower(this);
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
