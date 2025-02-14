using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeBase : ModifierBase
{
    public override bool CanAddModifier(Tower recipient)
    {
        return true;
    }
}