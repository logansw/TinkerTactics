using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModifierBase : MonoBehaviour
{
    void Start()
    {
        ModifierProcessor modifierProcessor = gameObject.GetComponent<ModifierProcessor>();
        modifierProcessor.TryAddModifier(this, gameObject.GetComponent<Tower>());
    }
    public abstract bool TryAddModifier(Tower recipient);
    public abstract void OnModifierAdded(Tower recipient);
    public virtual void ApplyDamageModifier(StatDamage stat) { }
    public virtual void ApplyRangeModifier(StatRange stat) { }
    public virtual void ApplySweepModifier(StatSweep stat) { }
    public virtual void ApplyAmmoModifier(StatAmmo stat) { }
}