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

    public void AddTower(Tower tower, Vector3 position)
    {
        Towers.Add(Instantiate(tower, position, Quaternion.identity));
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