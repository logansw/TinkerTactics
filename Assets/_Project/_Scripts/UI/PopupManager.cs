using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{
    [SerializeField] private TowerDialog _towerDialog;
    [SerializeField] private IconTooltip _iconTooltip;
    [SerializeField] private RangeGraphic _rangeGraphic;

    public void ShowTowerDialogBattle(Tower tower)
    {
        _towerDialog.Initialize(tower);
        _towerDialog.gameObject.SetActive(true);
    }

    public void HideTowerDialogBattle()
    {
        _towerDialog.gameObject.SetActive(false);
    }

    public void ShowIconTooltip(ITooltipTargetable tooltipSource)
    {
        _iconTooltip.Initialize(tooltipSource);
        _iconTooltip.gameObject.SetActive(true);
    }

    public void HideIconTooltip()
    {
        _iconTooltip.gameObject.SetActive(false);
    }

    public void ShowRangeGraphic(TowerRangeData towerRangeData)
    {
        _rangeGraphic.gameObject.SetActive(true);
        _rangeGraphic.DrawRangeIndicator(towerRangeData);
    }

    public void HideRangeGraphic()
    {
        _rangeGraphic.DestroyRangeIndicator();
        _rangeGraphic.gameObject.SetActive(false);
    }
}
