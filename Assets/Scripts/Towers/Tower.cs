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
    private LineRenderer _rangeIndicator;
    protected IAbility attack;
    protected RangeHandle _rangeHandle;

    public abstract string GetTooltipText();

    protected virtual void Awake()
    {
        _rangeIndicator = GetComponentInChildren<LineRenderer>();
        _rangeIndicator.useWorldSpace = false;
        HideRangeIndicator();
        _rangeHandle = GetComponentInChildren<RangeHandle>();
    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {

    }

    public void OnSelect()
    {
        ShowRangeIndicator(attack);
    }

    public void OnDeselect()
    {
        HideRangeIndicator();
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

    private void ShowRangeIndicator(IAbility ability)
    {
        _rangeIndicator.enabled = true;
        int arcPoints = 30;
        _rangeIndicator.positionCount = arcPoints + 2;
        // First Point
        _rangeIndicator.SetPosition(0, Vector2.zero);
        // Arc Points
        float angle = ability.Sweep * Mathf.PI / 180f;
        float startAngle = -angle / 2f;
        float angleDelta = angle / (arcPoints - 1);
        for (int i = 0; i < arcPoints; i++)
        {
            float x = ability.Range * Mathf.Cos(startAngle + angleDelta * i);
            float y = ability.Range * Mathf.Sin(startAngle + angleDelta * i);
            _rangeIndicator.SetPosition(i+1, new Vector3(x, y, 0));
        }
        // Last Point
        _rangeIndicator.SetPosition(_rangeIndicator.positionCount-1, Vector2.zero);
    }

    private void HideRangeIndicator()
    {
        // _rangeIndicator.enabled = false;
    }
}
