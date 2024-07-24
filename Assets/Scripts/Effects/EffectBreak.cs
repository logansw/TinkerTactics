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
    }

    public float GetSpeedMultiplier()
    {
        return 0.5f;
    }
}