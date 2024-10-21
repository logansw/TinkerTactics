using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierProcessor : MonoBehaviour
{
    private List<ModifierBase> _modifiers;
    private List<ModifierBase> Modifiers
    {
        get
        {
            if (_modifiers == null)
            {
                _modifiers = new List<ModifierBase>();
            }
            return _modifiers;
        }
        set
        {
            _modifiers = value;
        }
    }
    public int TinkerCount;
    public int WidgetCount;
    public Action e_OnModifierAdded;

    public void TryAddModifier(ModifierBase modifier, Tower recipient)
    {
        if (modifier.TryAddModifier(recipient))
        {
            AddModifier(modifier, recipient);
        }
    }

    public void AddModifier(ModifierBase modifier, Tower recipient)
    {
        Modifiers.Add(modifier);
        e_OnModifierAdded?.Invoke();
        modifier.OnModifierAdded(recipient);
    }

    public float CalculateDamage(StatDamage damage)
    {
        damage.Reset();
        foreach (ModifierBase modifier in Modifiers)
        {
            modifier.ApplyDamageModifier(damage);
        }
        damage.CalculatedFinal = damage.Current;
        return damage.CalculatedFinal;
    }

    public float CalculateRange(StatRange range)
    {
        range.Reset();
        foreach (ModifierBase modifier in Modifiers)
        {
            modifier.ApplyRangeModifier(range);
        }
        range.CalculatedFinal = range.Current;
        return range.CalculatedFinal;
    }

    public float CalculateSweep(StatSweep sweep)
    {
        sweep.Reset();
        foreach (ModifierBase modifier in Modifiers)
        {
            modifier.ApplySweepModifier(sweep);
        }
        sweep.CalculatedFinal = sweep.Current;
        return sweep.CalculatedFinal;
    }

    public int CalculateMaxAmmo(StatAmmo ammo)
    {
        ammo.Reset();
        foreach (ModifierBase modifier in Modifiers)
        {
            modifier.ApplyAmmoModifier(ammo);
        }
        ammo.CalculatedFinal = ammo.Current;
        return ammo.CalculatedFinal;
    }

    public float CalculateAttackSpeed(StatAttackSpeed attackSpeed)
    {
        attackSpeed.Reset();
        foreach (ModifierBase modifier in Modifiers)
        {
            modifier.ApplyAttackSpeedModifier(attackSpeed);
        }
        attackSpeed.CalculatedFinal = attackSpeed.Current;
        return attackSpeed.CalculatedFinal;
    }

    public float CalculateReloadSpeed(StatReloadSpeed reloadSpeed)
    {
        reloadSpeed.Reset();
        foreach (ModifierBase modifier in Modifiers)
        {
            modifier.ApplyReloadSpeedModifier(reloadSpeed);
        }
        reloadSpeed.CalculatedFinal = reloadSpeed.Current;
        return reloadSpeed.CalculatedFinal;
    }
}