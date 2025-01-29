using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotAssigner : MonoBehaviour
{
    private TilePlot _tilePlot;
    public TilePlot TilePlot { get; private set; }
    private Tower _tower;

    void Awake()
    {
        _tower = GetComponent<Tower>();
    }

    /// <summary>
    /// Assigns the tower to a tile plot by performing a raycast from the tower's position.
    /// If an empty tile plot is found, the tower is assigned to it. 
    /// If an occupied tile plot is found, the towers are swapped.
    /// If no tile plot is found, the tower is returned to its previous position.
    /// </summary>
    public void AssignToPlotBelow()
    {
        TilePlot closestPlot = TilePlot.GetClosest(gameObject);
        if (closestPlot == null)
        {
            // Return to plot if no tile plot is found
            TilePlot.AddTower(_tower);
            ReturnToPlot();
        }
        else if (closestPlot.AddTower(_tower))
        {
            TilePlot = closestPlot;
        }
        else
        {
            // Swap Towers
            Tower otherTower = closestPlot.Towers[0];
            TilePlot temp = TilePlot;
            closestPlot.RemoveTower(otherTower);
            closestPlot.AddTower(_tower);
            TilePlot.RemoveTower(_tower);
            TilePlot.AddTower(otherTower);
            TilePlot = closestPlot;
            otherTower.PlotAssigner.TilePlot = temp;
        }
    }

    private void ReturnToPlot()
    {
        transform.position = TilePlot.transform.position;
    }
}