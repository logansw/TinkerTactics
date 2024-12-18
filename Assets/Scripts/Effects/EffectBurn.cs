using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBurn : Effect, ITickEffect
{
    private InternalClock _internalClock;

    public override void Initialize(float duration, int stacks, EffectTracker effectTracker)
    {
        base.Initialize(duration, stacks, effectTracker);
        Stacks = 1;
        IconColor = new Color32(255, 107, 65, 255);
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

    public void OnTick(Enemy enemy)
    {
        enemy.ReceiveDamage(enemy.Health.MaxHealth * 0.01f);
    }

    public override string GetStackText()
    {
        return Stacks.ToString();
    }

    public override string GetAbbreviationText()
    {
        return "BRN";
    }

    public override string GetDescriptionText()
    {
        return $"Enemy takes 2% Max Health damage each second";
    }
}