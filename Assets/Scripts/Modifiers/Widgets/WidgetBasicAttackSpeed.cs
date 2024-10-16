using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetBasicAttackSpeed : WidgetBase
{
    public const float ATTACK_SPEED_MOD = 1.2f;

    public override void ApplyAttackSpeedModifier(StatAttackSpeed stat)
    {
        stat.Current *= ATTACK_SPEED_MOD;
    }

    public override string GetDescription()
    {
        return $"Increase Tower Attack Speed by {ATTACK_SPEED_MOD}x";
    }
}