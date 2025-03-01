using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinkerExecute : Tinker
{
    [SerializeField] private int _executeAmount;
    public override string GetDescription()
    {
        return $"Give Tower +{_executeAmount} Execute";
    }

    public override void ApplyEffects(EffectProcessor effectProcessor)
    {
        base.ApplyEffects(effectProcessor);
        effectProcessor.AddEffect(new ChangeBasicAttackEffect<ExecuteProjectileAttribute>(_executeAmount));
    }
}