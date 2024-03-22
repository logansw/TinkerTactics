using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBreak : Effect
{
    void Awake()
    {
        Unit = GetComponent<Unit>();
        Duration = 1.0f;
        Unit.DamageMultipliers.Add(3f);
        Unit.MovementSpeed = 0f;
    }

    public override void Remove()
    {
        Unit.DamageMultipliers.Remove(3f);
        Unit.MovementSpeed = Unit.UnitSO.MovementSpeed;
        base.Remove();
    }
}
