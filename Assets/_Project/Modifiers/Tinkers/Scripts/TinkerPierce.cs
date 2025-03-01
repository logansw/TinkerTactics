using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinkerPierce : Tinker
{
    [SerializeField] private int _pierceAmount;
    public override string GetDescription()
    {
        return $"Give Tower +{_pierceAmount} Pierce";
    }

    public override void ApplyEffects(EffectProcessor effectProcessor)
    {
        base.ApplyEffects(effectProcessor);
        effectProcessor.AddEffect(new ChangeBasicAttackEffect<PierceProjectileAttribute>(_pierceAmount));
    }
}
