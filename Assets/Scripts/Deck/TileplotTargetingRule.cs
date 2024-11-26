using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileplotTargetingRule : TargetingRules
{
    private bool TargetEmptyTilePlot;
    private bool TargetOccupiedTilePlot;
    public TilePlot TargetTilePlot;

    public TileplotTargetingRule(bool targetEmptyTilePlot, bool targetOccupiedTilePlot, CardEffect cardEffect)
    {
        TargetEmptyTilePlot = targetEmptyTilePlot;
        TargetOccupiedTilePlot = targetOccupiedTilePlot;
        e_OnCardReturned += cardEffect.OnCardReturned;
    }

    protected override bool ValidTargetHelper(Vector3 targetPosition)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(targetPosition, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.GetComponent<TilePlot>() != null)
            {
                TilePlot targetPlot = hit.collider.GetComponent<TilePlot>();
                if (TargetEmptyTilePlot && targetPlot.Towers.Count == 0)
                {
                    TargetTilePlot = targetPlot;
                    return true;
                }
                if (TargetOccupiedTilePlot && targetPlot.Towers.Count > 0)
                {
                    TargetTilePlot = targetPlot;
                    return true;
                }
            }
        }
        TargetTilePlot = null;
        return false;
    }
}
