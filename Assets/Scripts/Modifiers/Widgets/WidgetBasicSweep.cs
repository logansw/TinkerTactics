using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetBasicSweep : WidgetBase
{
    public const int SWEEP_MOD = 30;

    public override void ApplySweepModifier(StatSweep sweep)
    {
        sweep.Current += SWEEP_MOD;
    }

    public override string GetDescription()
    {
        return $"Increase Tower Sweep by {SWEEP_MOD} degrees";
    }
}