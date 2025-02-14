using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModifierBase : MonoBehaviour, ITooltipTargetable
{
    protected Tower _tower;
    public Sprite Icon;
    public string Name;
    public abstract bool CanAddModifier(Tower recipient);
    /// <summary>
    /// Subscribe to relevant TowerEvents here.
    /// </summary>
    /// <param name="recipient"></param>
    public abstract void Initialize(Tower recipient);
    public abstract string GetDescription();
    protected virtual void OnModifierEquipped(ModifierEquippedEvent modifierEquippedEvent) { Debug.LogError("Not Implemented");}

    public Sprite GetIcon()
    {
        return Icon;
    }

    public string GetTooltipName()
    {
        return Name;
    }

    public string GetTooltipDescription()
    {
        return GetDescription();
    }
}