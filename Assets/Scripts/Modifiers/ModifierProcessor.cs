using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierProcessor : MonoBehaviour
{
    public List<ModifierBase> Modifiers = new List<ModifierBase>();
    public int TinkerCount;
    public int WidgetCount;

    public void TryAddModifier(ModifierBase modifier, Tower recipient)
    {
        if (modifier.TryAddModifier(recipient))
        {
            Modifiers.Add(modifier);
            modifier.OnModifierAdded(recipient);
        }
    }

    public float CalculateDamage(StatDamage damage)
    {
        damage.Reset();
        foreach (ModifierBase modifier in Modifiers)
        {
            modifier.ApplyDamageModifier(damage);
        }
        return damage.Current;
    }
}