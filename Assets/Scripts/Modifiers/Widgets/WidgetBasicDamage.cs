using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetBasicDamage : WidgetBase
{
    public override void ApplyDamageModifier(StatDamage damage)
    {
        damage.Current += 1;
    }
}
