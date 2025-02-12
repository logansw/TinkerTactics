using UnityEngine;

public interface ITooltipTargetable
{
    public Sprite GetIcon();
    public string GetTooltipName();
    public string GetTooltipDescription();
}

