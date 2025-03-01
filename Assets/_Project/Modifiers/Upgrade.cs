using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour, IEffectSource
{
    protected Tower _tower;
    public List<Effect> Effects { get; set; }
    public bool CanApply(Tower recipient)
    {
        return true;
    }
    public abstract void ApplyEffects(EffectProcessor effectProcessor);
    public abstract string GetDescription();
    public abstract void Initialize(Tower recipient);
}