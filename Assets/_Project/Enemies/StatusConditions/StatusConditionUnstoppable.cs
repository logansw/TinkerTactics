using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatusConditionUnstoppable : StatusCondition
{
    public override void Initialize(float duration, int stacks, StatusConditionTracker statusConditionTracker)
    {
        base.Initialize(duration, stacks, statusConditionTracker);
        IconColor = new Color32(201, 195, 177, 255);
    }

    public override void AddStacks(int count)
    {
        Stacks += count;
    }

    public override void RemoveStacks(int count)
    {
        // Does not stack
    }

    public override string GetStackText()
    {
        return Stacks.ToString();
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