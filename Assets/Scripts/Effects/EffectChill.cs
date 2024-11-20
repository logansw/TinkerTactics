using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectChill : Effect
{
    private InternalClock _internalClock;

    public override void Initialize(float duration, int stacks, EffectTracker effectTracker)
    {
        base.Initialize(duration, stacks, effectTracker);
        Stacks = 0;
        AddStacks(stacks);
        IconColor = new Color32(128, 249, 255, 255);
        _internalClock = new InternalClock(duration);
        _internalClock.e_OnTimerDone += RemoveStack;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _internalClock.e_OnTimerDone -= RemoveStack;
        _internalClock.Delete();
    }

    public override void AddStacks(int count)
    {
        Stacks += count;
        if (Stacks >= 5)
        {
            Stacks = 5;
        }
        _effectTracker.UpdateRenderer();
    }

    public override void RemoveStacks(int count)
    {
        _effectTracker.UpdateRenderer();
        Stacks -= count;
        if (Stacks <= 0)
        {
            Stacks = 0;
            _effectTracker.RemoveEffect(this);
        }
    }

    private void RemoveStack()
    {
        RemoveStacks(1);
    }

    public float GetSpeedMultiplier()
    {
        return Mathf.Pow(0.8f, Stacks);
    }

    public override string GetStackText()
    {
        return Stacks.ToString();
    }

    public override string GetAbbreviationText()
    {
        return "CHL";
    }

    public override string GetDescriptionText()
    {
        return $"Slows the enemy by {GetSpeedMultiplier()}x";
    }
}