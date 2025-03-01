using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Tinker : MonoBehaviour, IEffectSource, ITooltipTargetable
{
    protected Tower _tower;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    
    public void Initialize(Tower tower)
    {
        _tower = tower;
    }

    public bool CanApply(Tower recipient)
    {
        bool result = recipient.TinkerCount < recipient.TinkerLimit;
        if (!result)
        {
            ToastManager.s_Instance.AddToast($"{recipient.Name} has reached the maximum number of Tinkers.");
        }
        return result;
    }
    
    public virtual void ApplyEffects(EffectProcessor effectProcessor)
    {
        _tower.TinkerCount++;
    }
    public abstract string GetDescription();
    public Sprite GetIcon()
    {
        return _icon;
    }
    public string GetTooltipDescription()
    {
        return GetDescription();
    }
    public string GetTooltipName()
    {
        return _name;
    }
}
