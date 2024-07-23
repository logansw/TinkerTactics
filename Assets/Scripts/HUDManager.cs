using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : Singleton<HUDManager>
{
    [SerializeField] private Canvas _HUDCanvas;
    [SerializeField] private GameObject TowerInfoPanelPrefab;
    private Dictionary<Tower, GameObject> _towerInfoPanels = new Dictionary<Tower, GameObject>();

    public void DisplayTowerInformation(Tower tower)
    {
        if (_towerInfoPanels.ContainsKey(tower))
        {
            GameObject towerHUD = _towerInfoPanels[tower];
            towerHUD.SetActive(true);
            towerHUD.transform.position = Camera.main.WorldToScreenPoint(tower.transform.position);
        }
        else
        {
            GameObject towerInfoPanel = Instantiate(TowerInfoPanelPrefab, Camera.main.WorldToScreenPoint(tower.transform.position), Quaternion.identity);
            towerInfoPanel.transform.SetParent(_HUDCanvas.transform);
            _towerInfoPanels.Add(tower, towerInfoPanel);
        }
    }

    public void HideTowerInformation(Tower tower)
    {
        _towerInfoPanels[tower].SetActive(false);
    }
}