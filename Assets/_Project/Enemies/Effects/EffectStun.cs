using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectStun : Effect, IMoveEffect
{
    private InternalClock _internalClock;

    public override void Initialize(float duration, int stacks, EffectTracker effectTracker)
    {
        base.Initialize(duration, stacks, effectTracker);
        if (Enemy.EffectTracker.HasEffect<EffectUnstoppable>(out EffectUnstoppable effectUnstoppable))
        {
            Enemy.EffectTracker.RemoveEffect(this);
            return;
        }
        IconColor = new Color32(255, 240, 128, 255);
        _internalClock = new InternalClock(duration, gameObject);
        _internalClock.e_OnTimerDone += Remove;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _internalClock.e_OnTimerDone -= Remove;
        _internalClock.Delete();
    }

    public float OnMove(float moveSpeed)
    {
        return 0;
    }

    public override void AddStacks(int count)
    {
        Stacks += count;
    }

    public override void RemoveStacks(int count)
    {
        Stacks -= count;
    }

    public override bool CheckRules()
    {
        if (Enemy.EffectTracker.HasEffect<EffectUnstoppable>(out EffectUnstoppable effectUnstoppable))
        {
            return false;
        }
        return true;
    }

    public override string GetStackText()
    {
        return Stacks.ToString();
    }

    public override string GetAbbreviationText()
    {
        return "STN";
    }

    public override string GetDescriptionText()
    {
        return $"";
    }

    public void Extend()
    {
        _internalClock.Reset();
    }
}
