using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : Singleton<HUDManager>
{
    [SerializeField] private Canvas _HUDCanvas;
    [SerializeField] private TowerInfoPanel TowerInfoPanelPrefab;
    private Dictionary<Tower, TowerInfoPanel> _towerInfoPanels = new Dictionary<Tower, TowerInfoPanel>();

    public void DisplayTowerInformation(Tower tower)
    {
        TowerInfoPanel towerInfoPanel;
        if (_towerInfoPanels.ContainsKey(tower))
        {
            towerInfoPanel = _towerInfoPanels[tower];
        }
        else
        {
            towerInfoPanel = Instantiate(TowerInfoPanelPrefab);
            towerInfoPanel.transform.SetParent(_HUDCanvas.transform);
            _towerInfoPanels.Add(tower, towerInfoPanel);
            towerInfoPanel.Initialize(tower);
        }
        towerInfoPanel.gameObject.SetActive(true);
        towerInfoPanel.transform.position = Camera.main.WorldToScreenPoint(tower.transform.position);
        towerInfoPanel.Render();
    }

    public void HideTowerInformation(Tower tower)
    {
        if (!_towerInfoPanels.ContainsKey(tower))
        {
            return;
        }
        _towerInfoPanels[tower].gameObject.SetActive(false);
    }

    public void DisplayAllTowerInfo()
    {
        foreach (Tower tower in _towerInfoPanels.Keys)
        {
            DisplayTowerInformation(tower);
        }
    }

    public void HideAllTowerInfo()
    {
        foreach (Tower tower in _towerInfoPanels.Keys)
        {
            HideTowerInformation(tower);
        }
    }
}