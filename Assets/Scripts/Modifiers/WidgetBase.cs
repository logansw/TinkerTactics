using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetBase : ModifierBase
{
    public override bool TryAddModifier(Tower recipient)
    {
        return true;
    }

    public override void OnModifierAdded(Tower recipient)
    {
        recipient.ModifierProcessor.WidgetCount++;
    }
}
