using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectChill : Effect, IMoveEffect
{
    private InternalClock _internalClock;

    public override void Initialize(float duration, int stacks, EffectTracker effectTracker)
    {
        base.Initialize(duration, stacks, effectTracker);
        Stacks = 1;
        IconColor = new Color32(128, 249, 255, 255);
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

    public float OnMove(float moveSpeed)
    {
        return moveSpeed * 0.5f;
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
        return $"Slows the enemy by 0.5x";
    }
}