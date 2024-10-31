using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectStun : Effect
{
    private InternalClock _internalClock;

    public override void Initialize(int duration)
    {
        base.Initialize(duration);
        if (Enemy.EffectTracker.HasEffect<EffectUnstoppable>(out EffectUnstoppable effectUnstoppable))
        {
            Enemy.EffectTracker.RemoveEffect(this);
            return;
        }
        Duration = duration;
        IconColor = new Color32(255, 240, 128, 255);
        _internalClock = new InternalClock(0.5f);
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
        Duration += count;
    }

    public override void RemoveStacks(int count)
    {
        Duration -= count;
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
        return Duration.ToString();
    }

    public override string GetAbbreviationText()
    {
        return "STN";
    }

    public override string GetDescriptionText()
    {
        return $"STUN: Enemy cannot act for {Duration} turns.";
    }
}
