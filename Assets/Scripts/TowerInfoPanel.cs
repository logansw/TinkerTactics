using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoPanel : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private TMP_Text _energyText;

    public void Initialize(Tower tower)
    {
        _tower = tower;
        Render();
    }

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    public void Render()
    {
        
    }

    public void DisplayTowerTooltip()
    {
        TooltipManager.s_Instance.DisplayTooltip(_tower.GetTooltipText());
    }

    public void HideToolTip()
    {
        ShowRangeIndicator(false, 0);
        TooltipManager.s_Instance.HideTooltip();
    }

    public void ShowRangeIndicator(bool active, float range)
    {
        
    }
}
