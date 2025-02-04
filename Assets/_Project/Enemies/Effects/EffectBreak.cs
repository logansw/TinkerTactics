using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectBreak : Effect
{
    private InternalClock _internalClock;

    public override void Initialize(float duration, int stacks, EffectTracker effectTracker)
    {
        base.Initialize(duration, stacks, effectTracker);
        Enemy.EffectTracker.AddEffect<EffectStun>(duration, stacks);
        Enemy.EffectTracker.AddEffect<EffectVulnerable>(duration, stacks);
        IconColor = new Color32(255, 158, 128, 255);
        _internalClock = new InternalClock(duration, gameObject);
        _internalClock.e_OnTimerDone += Remove;
    }

    public override void AddStacks(int count)
    {
        // Break does not stack
    }

    public override void RemoveStacks(int count)
    {
        // Break does not stack
    }

    public float GetSpeedMultiplier()
    {
        return 0.5f;
    }

    public override string GetStackText()
    {
        return "";
    }

    public override string GetAbbreviationText()
    {
        return "BRK";
    }

    public override string GetDescriptionText()
    {
        return "BREAK: Applies Stun and Vulnerable.";
    }

    public override void Remove()
    {
        if (Enemy is Warlord)
        {
            Warlord warlord = Enemy.gameObject.GetComponent<Warlord>();
            warlord.Retreat();
            _internalClock.e_OnTimerDone -= Remove;
            _internalClock.Delete();
        }
        base.Remove();
    }

    public void Extend()
    {
        _internalClock.Reset();
        Enemy.EffectTracker.HasEffect<EffectStun>(out EffectStun effectStun);
        effectStun.Extend();
        Enemy.EffectTracker.HasEffect<EffectVulnerable>(out EffectVulnerable effectVulnerable);
        effectVulnerable.Extend();
    }
}