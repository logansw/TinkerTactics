using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Singleton<TowerManager>
{
    public List<Tower> Towers;

    public override void Initialize()
    {
        base.Initialize();
        Towers = new List<Tower>();
    }

    public Tower AddTower(Tower tower, Vector3 position)
    {
        Tower newTower = Instantiate(tower, position, Quaternion.identity);
        Towers.Add(newTower);
        return newTower;
    }

    public void RemoveTower(Tower tower)
    {
        Towers.Remove(tower);
        tower.TilePlot.RemoveTower(tower);
        tower.Activate(false);
        // Destroy(tower.gameObject);
    }

    public void ClearTowers()
    {
        foreach (Tower tower in Towers)
        {
            Destroy(tower.gameObject);
        }
        Towers.Clear();
    }
}