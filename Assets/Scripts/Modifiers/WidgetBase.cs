using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WidgetBase : ModifierBase
{
    public override bool CanAddModifier(Tower recipient)
    {
        return true;
    }

    public override void OnModifierAdded(Tower recipient)
    {
        recipient.ModifierProcessor.WidgetCount++;
    }
}
