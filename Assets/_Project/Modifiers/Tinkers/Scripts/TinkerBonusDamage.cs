using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinkerBonusDamage : Tinker
{
    public float DamageMultiplier;

    public override string GetDescription()
    {
        return $"Deal {DamageMultiplier * 100 - 100}% bonus damage to Boss enemies";
    }

    public override void ApplyEffects(EffectProcessor effectProcessor)
    {
        base.ApplyEffects(effectProcessor);
        effectProcessor.AddEffect(new SelectiveBonusDamageEffect<Warlord>(0, DamageMultiplier));
    }
}