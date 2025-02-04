using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerManager : Singleton<TowerManager>
{
    public List<Tower> Towers;
    public int TowerDeployLimit;
    [SerializeField] private TMP_Text _towersDeployedText;


    public override void Initialize()
    {
        base.Initialize();
        Towers = new List<Tower>();
    }

    public Tower AddTower(Tower tower, Vector3 position)
    {
        Tower newTower = Instantiate(tower, position, Quaternion.AngleAxis(45f, Vector3.forward), transform);
        Towers.Add(newTower);
        RenderTowerCount();
        return newTower;
    }

    public void RemoveTower(Tower tower)
    {
        Towers.Remove(tower);
        tower.PlotAssigner.TilePlot.RemoveTower(tower);
        tower.gameObject.SetActive(false);
        RenderTowerCount();
    }

    public void ClearTowers()
    {
        int count = Towers.Count;
        for (int i = count - 1; i >= 0; i--)
        {
            if (Towers[i].gameObject.activeSelf)
            {
                Towers[i].Recall();
            }
        }
        Towers.Clear();
        RenderTowerCount();
    }

    public void RenderTowerCount()
    {
        _towersDeployedText.text = $"Towers Deployed: {Towers.Count}/{TowerDeployLimit}";
    }
}