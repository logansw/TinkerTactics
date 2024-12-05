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
        tower.gameObject.SetActive(false);
    }

    public void ClearTowers()
    {
        int count = Towers.Count;
        for (int i = count - 1; i >= 0; i--)
        {
            Towers[i].Recall();
        }
        Towers.Clear();
    }
}