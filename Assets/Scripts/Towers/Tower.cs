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
    public TilePlot TilePlot;

    void Awake()
    {
        ProjectileLauncher = GetComponent<ProjectileLauncher>();
        TargetTracker = GetComponent<TargetTracker>();
    }

    void Start()
    {
        AssignTowerToTilePlot();
    }

    public virtual void Update()
    {
        if (CanAttack())
        {
            Attack();
        }
    }

    private bool CanAttack()
    {
        return TilePlot.IsActivated && 
                StateController.s_Instance.CurrentState == StateType.BattleState &&
                ProjectileLauncher.CanAttack() &&
                TargetTracker.HasEnemiesInRange();
        }

    private void Attack()
    {
        ProjectileLauncher.LaunchProjectile(TargetTracker.GetHighestPriorityTarget());
    }

    public void OnSelect()
    {
        // TargetTracker.DisplayRange(true);
    }

    public void OnDeselect()
    {
        TargetTracker.DisplayRange(false);
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
                    TilePlot thisTowerOldPlot = TilePlot;

                    // Swap the towers
                    otherPlot.Tower = this;
                    thisTowerOldPlot.Tower = otherTower;

                    // Update the _tilePlot references
                    this.TilePlot = otherPlot;
                    otherTower.TilePlot = thisTowerOldPlot;

                    // Return the towers to their new plots
                    otherTower.ReturnToPlot();
                    ReturnToPlot();
                    return;
                }
                else
                {
                    TilePlot.Tower = null;
                    TilePlot = otherPlot;
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
        transform.position = TilePlot.transform.position;
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
