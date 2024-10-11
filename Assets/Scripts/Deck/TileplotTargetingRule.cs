using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileplotTargetingRule : TargetingRules
{
    private bool TargetEmptyTilePlot;
    private bool TargetOccupiedTilePlot;

    public TileplotTargetingRule(bool targetEmptyTilePlot, bool targetOccupiedTilePlot)
    {
        TargetEmptyTilePlot = targetEmptyTilePlot;
        TargetOccupiedTilePlot = targetOccupiedTilePlot;
    }

    public override bool ValidTarget(Vector3 targetPosition)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(targetPosition, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.GetComponent<TilePlot>() != null)
            {
                TilePlot targetPlot = hit.collider.GetComponent<TilePlot>();
                if (TargetEmptyTilePlot && targetPlot.Towers.Count == 0)
                {
                    return true;
                }
                if (TargetOccupiedTilePlot && targetPlot.Towers.Count > 0)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
