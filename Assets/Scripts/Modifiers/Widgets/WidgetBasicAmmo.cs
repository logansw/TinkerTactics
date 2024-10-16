using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetBasicAmmo : WidgetBase
{
    public const int AMMO_MOD = 2;

    public override void ApplyAmmoModifier(StatAmmo ammo)
    {
        ammo.Current += AMMO_MOD;
    }

    public override string GetDescription()
    {
        return $"Increase Tower Ammo by {AMMO_MOD}";
    }
}