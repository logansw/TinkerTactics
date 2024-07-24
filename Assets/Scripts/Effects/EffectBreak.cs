using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectBreak : Effect
{
    public override void Initialize(int duration)
    {
        base.Initialize(duration);
        Duration = duration;
        Enemy.EffectTracker.AddEffect<EffectStun>(duration);
        Enemy.EffectTracker.AddEffect<EffectVulnerable>(duration);
        IconColor = new Color32(255, 158, 128, 255);
    }

    public float GetSpeedMultiplier()
    {
        return 0.5f;
    }

    public override string GetStackText()
    {
        return "";
    }

    public override string GetAbbreviationText()
    {
        return "BRK";
    }
}