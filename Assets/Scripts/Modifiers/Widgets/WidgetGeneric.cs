using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class WidgetGeneric : WidgetBase
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
            sb.Append($"Increase Tower Damage by {DamageModFlat}\n");
        }
        if (RangeModFlat != 0)
        {
            sb.Append($"Increase Tower Range by {RangeModFlat}\n");
        }
        if (SweepModFlat != 0)
        {
            sb.Append($"Increase Tower Sweep by {SweepModFlat} degrees\n");
        }
        if (AmmoModFlat != 0)
        {
            sb.Append($"Increase Tower Ammo by {AmmoModFlat}\n");
        }
        if (ReloadSpeedModMult != 0)
        {
            sb.Append($"Increase Tower Reload Speed by {ReloadSpeedModMult}x\n");
        }
        if (AttackSpeedModMult != 0)
        {
            sb.Append($"Increase Tower Attack Speed by {AttackSpeedModMult}x\n");
        }
        return sb.ToString();
    }

    public override void ApplyDamageModifier(StatDamage stat)
    {
        if (DamageModFlat == default) { return; }

        stat.Current += DamageModFlat;
    }

    public override void ApplyRangeModifier(StatRange stat)
    {
        if (RangeModFlat == default) { return; }
        stat.Current += RangeModFlat;
    }

    public override void ApplySweepModifier(StatSweep stat)
    {
        if (SweepModFlat == default) { return; }
        stat.Current += SweepModFlat;
    }

    public override void ApplyAmmoModifier(StatAmmo stat)
    {
        if (AmmoModFlat == default) { return; }
        stat.Current += AmmoModFlat;
    }

    public override void ApplyReloadSpeedModifier(StatReloadSpeed stat)
    {
        if (ReloadSpeedModMult == default) { return; }
        stat.Current *= ReloadSpeedModMult;
    }

    public override void ApplyAttackSpeedModifier(StatAttackSpeed stat)
    {
        if (AttackSpeedModMult == default) { return; }
        stat.Current *= AttackSpeedModMult;
    }
}
