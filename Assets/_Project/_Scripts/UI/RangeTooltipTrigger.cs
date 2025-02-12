using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RangeTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TowerRangeData _towerRangeData;

    public void Initialize(TowerRangeData towerRangeData)
    {
        _towerRangeData = towerRangeData;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        PopupManager.s_Instance.ShowRangeGraphic(_towerRangeData);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        PopupManager.s_Instance.HideRangeGraphic();
    }
}
