using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TinkerBase : ModifierBase
{
    public override bool CanAddModifier(Tower recipient)
    {
        return recipient.ModifierProcessor.TinkerCount < 3;
    }

    public override void OnModifierAdded(Tower recipient)
    {
        recipient.ModifierProcessor.TinkerCount++;
    }
}
