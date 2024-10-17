using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetBasicRange : WidgetBase
{
    public int RANGE_MOD;
    public override void ApplyRangeModifier(StatRange stat)
    {
        stat.Current += RANGE_MOD;
    }

    public override string GetDescription()
    {
        return $"Increase Tower Range by {RANGE_MOD}";
    }
}
