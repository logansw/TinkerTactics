using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetBasicReloadSpeed : WidgetBase
{
    public const float RELOAD_SPEED_MOD = 1.2f;
    public override void ApplyReloadSpeedModifier(StatReloadSpeed stat)
    {
        stat.Current *= RELOAD_SPEED_MOD;
    }

    public override string GetDescription()
    {
        return $"Increase Tower Reload Speed by {RELOAD_SPEED_MOD}x";
    }
}
