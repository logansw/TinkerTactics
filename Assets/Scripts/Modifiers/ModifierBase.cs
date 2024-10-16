using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModifierBase : MonoBehaviour
{
    public virtual bool TryAddModifier(Tower recipient)
    {
        if (CanAddModifier(recipient))
        {
            recipient.ModifierProcessor.AddModifier(this);
            return true;
        }
        else
        {
            return false;
        }
    }
    public abstract string GetDescription();
    public abstract bool CanAddModifier(Tower recipient);
    public abstract void OnModifierAdded(Tower recipient);
    public virtual void ApplyDamageModifier(StatDamage stat) { }
    public virtual void ApplyRangeModifier(StatRange stat) { }
    public virtual void ApplySweepModifier(StatSweep stat) { }
    public virtual void ApplyAmmoModifier(StatAmmo stat) { }
    public virtual void ApplyAttackSpeedModifier(StatAttackSpeed stat) { }
    public virtual void ApplyReloadSpeedModifier(StatReloadSpeed stat) { }
}