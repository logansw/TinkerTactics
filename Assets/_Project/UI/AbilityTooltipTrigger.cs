using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class AbilityTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ITooltipTargetable _tooltipTargetable;

    public void Initialize(ITooltipTargetable tooltipTargetable)
    {
        _tooltipTargetable = tooltipTargetable;
        GetComponent<Image>().sprite = tooltipTargetable.GetIcon();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PopupManager.s_Instance.ShowIconTooltip(_tooltipTargetable);
    }
    public void OnPointerExit(PointerEventData eventData)    
    {
        PopupManager.s_Instance.HideIconTooltip();
    }
}