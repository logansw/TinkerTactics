using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class TinkerGeneric : TinkerBase
{
    public int DamageModFlat;
    public int AmmoModFlat;
    public float ReloadSpeedModMult;
    public float AttackSpeedModMult;
    public int CostModDelta;

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
        if (CostModDelta != 0)
        {
            if (CostModDelta > 0)
            {
                sb.Append($"+{CostModDelta} Energy Cost\n");
            }
            else
            {
                sb.Append($"-{CostModDelta} Energy Cost\n");
            }
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
        ApplyDamageModifier(recipient.Damage);
        ApplyReloadSpeedModifier(recipient.ReloadSpeed);
        ApplyAttackSpeedModifier(recipient.AttackSpeed);
        ApplyAmmoModifier(recipient.Ammo);
        ApplyCostModifier();
        recipient.BasicAttack.SetClocks();
    }

    public void ApplyDamageModifier(Stat stat)
    {
        if (DamageModFlat == default) { return; }

        stat.Current += DamageModFlat;
    }

    public void ApplyAmmoModifier(Stat ammo)
    {
        ammo.Max += AmmoModFlat;
        ammo.Reset();
    }

    public void ApplyReloadSpeedModifier(Stat stat)
    {
        if (ReloadSpeedModMult == default) { return; }
        stat.Current *= ReloadSpeedModMult;
    }

    public void ApplyAttackSpeedModifier(Stat stat)
    {
        if (AttackSpeedModMult == default) { return; }
        stat.Current *= AttackSpeedModMult;
    }

    public void ApplyCostModifier()
    {
        _tower.ParentCard.EnergyCost += CostModDelta;
    }
}
