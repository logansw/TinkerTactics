using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinkerKillReload : Tinker
{
    public override string GetDescription()
    {
        return "Killing an enemy reloads 1 ammo";   
    }

    public override void ApplyEffects(EffectProcessor effectProcessor)
    {
        base.ApplyEffects(effectProcessor);
        effectProcessor.AddEffect(new KillReloadEffect(1));
    }
}