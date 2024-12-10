using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class TinkerGeneric : TinkerBase
{
    public int DamageModFlat;
    public int RangeModFlat;
    public int SweepModFlat;
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
        if (RangeModFlat != 0)
        {
            sb.Append($"+{RangeModFlat} Range\n");
        }
        if (SweepModFlat != 0)
        {
            sb.Append($"+{SweepModFlat} Sweep\n");
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

    public override void Initialize(Tower recipient)
    {
        _tower = recipient;
        EventBus.Subscribe<TinkerEquippedEvent>(OnTinkerEquipped);
    }

    protected override void OnTinkerEquipped(TinkerEquippedEvent tinkerEquippedEvent)
    {
        Tower recipient = tinkerEquippedEvent.Tower;
        ApplyStatModifier(recipient);
        EventBus.Unsubscribe<TinkerEquippedEvent>(OnTinkerEquipped);
    }

    private void ApplyStatModifier(Tower recipient)
    {
        ApplyDamageModifier(recipient.BasicAttack.Damage);
        ApplyRangeModifier(recipient.Range);
        ApplySweepModifier(recipient.Sweep);
        ApplyReloadSpeedModifier(recipient.BasicAttack.ReloadSpeed);
        ApplyAttackSpeedModifier(recipient.BasicAttack.AttackSpeed);
        ApplyAmmoModifier(recipient.BasicAttack.MaxAmmo);
        recipient.BasicAttack.SetClocks();
    }

    public void ApplyDamageModifier(StatDamage stat)
    {
        if (DamageModFlat == default) { return; }

        stat.Current += DamageModFlat;
    }

    public void ApplyRangeModifier(StatRange stat)
    {
        if (RangeModFlat == default) { return; }
        stat.Current += RangeModFlat;
    }

    public void ApplySweepModifier(StatSweep stat)
    {
        if (SweepModFlat == default) { return; }
        stat.Current += SweepModFlat;
    }

    public void ApplyAmmoModifier(StatAmmo maxAmmo)
    {
        if (AmmoModFlat == default) { return; }
        maxAmmo.Current += AmmoModFlat;
        _tower.BasicAttack.CurrentAmmo.Base = maxAmmo.Current;
        _tower.BasicAttack.CurrentAmmo.Reset();
    }

    public void ApplyReloadSpeedModifier(StatReloadSpeed stat)
    {
        if (ReloadSpeedModMult == default) { return; }
        stat.Current *= ReloadSpeedModMult;
    }

    public void ApplyAttackSpeedModifier(StatAttackSpeed stat)
    {
        if (AttackSpeedModMult == default) { return; }
        stat.Current *= AttackSpeedModMult;
    }
}
