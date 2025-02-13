using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TinkerBase : ModifierBase
{
    public override bool CanAddModifier(Tower recipient)
    {
        bool result = recipient.ModifierProcessor.TinkerCount < recipient.TinkerLimit;
        if (!result)
        {
            ToastManager.s_Instance.AddToast($"{recipient.Name} has reached the maximum number of Tinkers.");
        }
        return result;
    }

    protected override void OnTinkerEquipped(TinkerEquippedEvent tinkerEquippedEvent)
    {
        tinkerEquippedEvent.Tower.ModifierProcessor.TinkerCount++;
    }
}
