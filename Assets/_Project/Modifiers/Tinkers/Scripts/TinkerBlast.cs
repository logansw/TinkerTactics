using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinkerBlast : Tinker
{
    [SerializeField] private int _blastAmount;
    public override string GetDescription()
    {
        return $"Give Tower +{_blastAmount} Blast";
    }

    public override void ApplyEffects(EffectProcessor effectProcessor)
    {
        base.ApplyEffects(effectProcessor);
        effectProcessor.AddEffect(new ChangeBasicAttackEffect<BlastProjectileAttribute>(_blastAmount));
    }
}