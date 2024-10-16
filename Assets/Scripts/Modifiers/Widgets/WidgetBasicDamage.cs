using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetBasicDamage : WidgetBase
{
    public const int DAMAGE_MOD = 1;

    public override void ApplyDamageModifier(StatDamage damage)
    {
        damage.Current += DAMAGE_MOD;
    }

    public override string GetDescription()
    {
        return $"Increase Tower Damage by {DAMAGE_MOD}";
    }
}