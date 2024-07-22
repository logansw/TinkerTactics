using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// High-level component for a tower. Contains the core logic for attacking enemies.
/// </summary>
[RequireComponent(typeof(ProjectileLauncher)), RequireComponent(typeof(TargetTracker))]
public abstract class Tower : MonoBehaviour, ISelectable, ILiftable
{
    [HideInInspector] public ProjectileLauncher ProjectileLauncher;
    [HideInInspector] public TargetTracker TargetTracker;
    private TilePlot _tilePlot;
    [HideInInspector] public List<List<Upgrade>> Upgrades;
    [HideInInspector] public List<Upgrade> SelectedUpgrades;
    [HideInInspector] public int Tier;

    protected virtual void Awake()
    {
        ProjectileLauncher = GetComponent<ProjectileLauncher>();
        TargetTracker = GetComponent<TargetTracker>();
    }

    protected virtual void Start()
    {
        Tier = 0;
    }

    protected bool CanAttack()
    {
        return _tilePlot.IsActivated && 
                ProjectileLauncher.CanAttack() &&
                TargetTracker.HasEnemiesInRange();
        }

    protected virtual void Attack()
    {
        ProjectileLauncher.LaunchProjectile(TargetTracker.GetHighestPriorityTarget());
    }

    public void OnSelect()
    {
        HUDManager.s_Instance.DisplayTowerInformation(this);
    }

    public void OnDeselect()
    {
        HUDManager.s_Instance.HideTowerInformation(this);
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
                if (otherPlot.IsOccupied)
                {
                    Tower otherTower = otherPlot.Tower;
                    TilePlot thisTowerOldPlot = _tilePlot;

                    // Swap the towers
                    otherPlot.Tower = this;
                    thisTowerOldPlot.Tower = otherTower;

                    // Update the _tilePlot references
                    this._tilePlot = otherPlot;
                    otherTower._tilePlot = thisTowerOldPlot;

                    // Return the towers to their new plots
                    otherTower.ReturnToPlot();
                    ReturnToPlot();
                    return;
                }
                else
                {
                    _tilePlot ??= otherPlot;
                    _tilePlot.Tower = null;
                    _tilePlot = otherPlot;
                    otherPlot.Tower = this;
                    ReturnToPlot();
                    return;
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
        // Do nothing
    }

    public void OnDrop()
    {
        AssignTowerToTilePlot();
    }

    public void OnHover()
    {
        // Do nothing
    }

    public void OnHeld()
    {
        // Do nothing
    }
}
