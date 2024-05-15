using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Obstacle : MonoBehaviour, ISelectable, ILiftable
{
    public abstract bool CanAttack();
    public abstract void Attack();
    public ObstacleSlot ObstacleSlot { get; set; }
    public Collider2D Collider2D { get; set; }

    protected virtual void Awake()
    {
        Collider2D = GetComponent<Collider2D>();
    }

    protected virtual void Start()
    {
        AssignToSlot();
    }

    public virtual void Update()
    {
        if (CanAttack())
        {
            Attack();
        }
    }


    public void OnSelect()
    {
        // TargetTracker.DisplayRange(true);
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
    public void AssignToSlot()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.GetComponent<ObstacleSlot>() != null)
            {
                ObstacleSlot otherSlot = hit.collider.GetComponent<ObstacleSlot>();
                if (otherSlot.IsOccupied)
                {
                    Obstacle otherObstacle = otherSlot.Obstacle;
                    ObstacleSlot thisTowerOldPlot = ObstacleSlot;

                    // Swap the towers
                    otherSlot.Obstacle = this;
                    thisTowerOldPlot.Obstacle = otherObstacle;

                    // Update the _tilePlot references
                    this.ObstacleSlot = otherSlot;
                    otherObstacle.ObstacleSlot = thisTowerOldPlot;

                    // Return the towers to their new plots
                    otherObstacle.ReturnToSlot();
                    ReturnToSlot();
                    return;
                }
                else
                {
                    if (ObstacleSlot != null)
                    {
                        ObstacleSlot.Obstacle = null;
                    }
                    ObstacleSlot = otherSlot;
                    otherSlot.Obstacle = this;
                    ReturnToSlot();
                    return;
                }
            }
        }
        // Return to plot if no tile plot is found
        ReturnToSlot();
    }

    private void ReturnToSlot()
    {
        transform.position = ObstacleSlot.transform.position;
    }

    public void OnLift()
    {
        // Do nothing
    }

    public void OnDrop()
    {
        AssignToSlot();
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
