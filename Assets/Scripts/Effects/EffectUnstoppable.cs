using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectUnstoppable : Effect
{
    public override void Initialize(int duration)
    {
        base.Initialize(duration);
        Duration = duration;
    }

    public override void AddStacks(int count)
    {
        Duration += count;
    }

    public override string GetStackText()
    {
        return Duration.ToString();
    }

    public override string GetAbbreviationText()
    {
        return "UNST";
    }

    public override string GetDescriptionText()
    {
        return $"UNSTOPPABLE: Enemy resists Stun and cannot have its Speed reduced.";
    }
}