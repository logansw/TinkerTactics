using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectBreak : Effect
{
    public void Initialize(int duration)
    {
        Unit = GetComponent<Unit>();
        Duration = duration;
        Unit.DamageMultipliers.Add(2f);
        EffectStun stun = Unit.AddComponent<EffectStun>();
        stun.Initialize(1);
    }

    public override void Remove()
    {
        Unit.DamageMultipliers.Remove(3f);
        Unit.MovementSpeed = Unit.UnitSO.MovementSpeed;
        base.Remove();
    }
}
