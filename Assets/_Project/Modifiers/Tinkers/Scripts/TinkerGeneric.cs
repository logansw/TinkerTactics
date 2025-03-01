using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class TinkerGeneric : Tinker
{
    public int DamageModFlat;
    public int AmmoModFlat;
    public float ReloadSpeedModMult;
    public float AttackSpeedModMult;

    public override string GetDescription()
    {
        StringBuilder sb = new StringBuilder();
        if (DamageModFlat != 0)
        {
            sb.Append($"+{DamageModFlat} Damage\n");
        }
        if (AmmoModFlat != 0)
        {
            sb.Append($"+{AmmoModFlat} Ammo\n");
        }
        if (ReloadSpeedModMult != 0)
        {
            sb.Append($"Reload Speed x{ReloadSpeedModMult}\n");
        }
        if (AttackSpeedModMult != 0)
        {
            sb.Append($"Attack Speed x{AttackSpeedModMult}\n");
        }
        return sb.ToString();
    }

    public override void ApplyEffects(EffectProcessor effectProcessor)
    {
        base.ApplyEffects(effectProcessor);
        ApplyDamageModifier(effectProcessor);
        ApplyReloadSpeedModifier(effectProcessor);
        ApplyAttackSpeedModifier(effectProcessor);
        ApplyAmmoModifier(effectProcessor);
        _tower.BasicAttack.SetClocks();
    }

    public void ApplyDamageModifier(EffectProcessor effectProcessor)
    {
        if (DamageModFlat == default) { return; }
        
        effectProcessor.AddEffect(new ChangeStatEffect(_tower.Damage, StatChangeType.Additive, DamageModFlat));
    }

    public void ApplyAmmoModifier(EffectProcessor effectProcessor)
    {
        if (AmmoModFlat == default) { return; }
        
        effectProcessor.AddEffect(new ChangeStatEffect(_tower.Ammo, StatChangeType.Additive, AmmoModFlat));
    }

    public void ApplyReloadSpeedModifier(EffectProcessor effectProcessor)
    {
        if (ReloadSpeedModMult == default) { return; }
        
        effectProcessor.AddEffect(new ChangeStatEffect(_tower.ReloadSpeed, StatChangeType.Multiplicative, ReloadSpeedModMult));
    }

    public void ApplyAttackSpeedModifier(EffectProcessor effectProcessor)
    {
        if (AttackSpeedModMult == default) { return; }
        
        effectProcessor.AddEffect(new ChangeStatEffect(_tower.AttackSpeed, StatChangeType.Multiplicative, AttackSpeedModMult));
    }
}
