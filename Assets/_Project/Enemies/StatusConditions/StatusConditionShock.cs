using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusConditionShock : StatusCondition, IDamageStatusCondition
{
    private InternalClock _internalClock;

    public override void Initialize(float duration, int stacks, StatusConditionTracker statusConditionTracker)
    {
        base.Initialize(duration, stacks, statusConditionTracker);
        Stacks = 1;
        IconColor = new Color32(254, 230, 127, 255);
        _internalClock = new InternalClock(duration, gameObject);
        _internalClock.e_OnTimerDone += Remove;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _internalClock.e_OnTimerDone -= Remove;
        _internalClock.Delete();
    }

    public override void AddStacks(int count)
    {
        Stacks = 1;
        _internalClock.Reset();
    }

    public override void RemoveStacks(int count)
    {
        throw new System.NotImplementedException();
    }

    public float OnDamage(float damage)
    {
        return damage * 1.25f;
    }

    public override string GetStackText()
    {
        return Stacks.ToString();
    }

    public override string GetAbbreviationText()
    {
        return "SHK";
    }

    public override string GetDescriptionText()
    {
        return $"Enemy takes 1.25x damage from all sources";
    }
}