using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectChill : Effect
{
    public static int FrostbiteDamage = 10;
    private InternalClock _internalClock;

    public override void Initialize(int stacks)
    {
        Stacks = 0;
        _internalClock = new InternalClock(3f);
        _internalClock.e_OnTimerDone += Remove;
        AddStacks(stacks);
        IconColor = new Color32(128, 249, 255, 255);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _internalClock.e_OnTimerDone -= Remove;
        _internalClock.Delete();
    }

    public override void AddStacks(int count)
    {
        _internalClock.Reset();
        for (int i = 0; i < count; i++)
        {
            Stacks++;
            if (Stacks == 2)
            {
                Enemy.EffectTracker.AddEffect<EffectStun>(1);
            }
            else if (Stacks >= 3)
            {
                Enemy.OnImpact(FrostbiteDamage);
                Remove();
            }
        }
    }

    public override void RemoveStacks(int count)
    {
        Stacks -= count;
    }

    public float GetSpeedMultiplier()
    {
        return 0.5f;
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
        return $"CHILL:\n(1) Reduces speed by 50%\n(2) Applies Stun\n(3+) deals {FrostbiteDamage} damage.";
    }
}