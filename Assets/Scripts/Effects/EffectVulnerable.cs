using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectVulnerable : Effect
{
    private InternalClock _internalClock;

    public override void Initialize(float duration, int stacks, EffectTracker effectTracker)
    {
        base.Initialize(duration, stacks, effectTracker);
        IconColor = new Color32(255, 128, 170, 255);
        _internalClock = new InternalClock(duration, gameObject);
        _internalClock.e_OnTimerDone += Remove;
    }

    public override void AddStacks(int count)
    {
        Stacks += count;
    }

    public override void RemoveStacks(int count)
    {
        Stacks -= count;
    }

    public float GetDamageMultiplier()
    {
        return 2f;
    }

    public override string GetStackText()
    {
        return Stacks.ToString();
    }

    public override string GetAbbreviationText()
    {
        return "VUL";
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