using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectVulnerable : Effect
{
    public override void Initialize(int duration, EffectTracker effectTracker)
    {
        base.Initialize(duration, effectTracker);
        Duration = duration;
        IconColor = new Color32(255, 128, 170, 255);
    }

    public override void AddStacks(int count)
    {
        Duration += count;
    }

    public override void RemoveStacks(int count)
    {
        Duration -= count;
    }

    public float GetDamageMultiplier()
    {
        return 2f;
    }

    public override string GetStackText()
    {
        return Duration.ToString();
    }

    public override string GetAbbreviationText()
    {
        return "VUL";
    }

    public override string GetDescriptionText()
    {
        return $"VULNERABLE: Enemy takes double damage for {Duration} turns.";
    }
}