using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectStun : Effect
{
    public override void Initialize(int duration)
    {
        base.Initialize(duration);
        if (Enemy.EffectTracker.HasEffect<EffectUnstoppable>(out EffectUnstoppable effectUnstoppable))
        {
            Remove();
        }
        Duration = duration;
        IconColor = new Color32(255, 240, 128, 255);
    }

    public override void AddStacks(int count)
    {
        Duration += count;
    }

    public override string GetStackText()
    {
        return Duration.ToString();
    }

    public override string GetAbbreviationText()
    {
        return "STN";
    }

    public override string GetDescriptionText()
    {
        return $"STUN: Enemy cannot act for {Duration} turns.";
    }
}
