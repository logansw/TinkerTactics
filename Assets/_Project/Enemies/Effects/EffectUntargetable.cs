using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUntargetable : Effect
{
    public override void AddStacks(int count)
    {
        Stacks += count;
    }

    public override void RemoveStacks(int count)
    {
        Stacks -= count;
    }

    public override string GetStackText()
    {
        return "";
    }

    public override string GetAbbreviationText()
    {
        return "";
    }

    public override string GetDescriptionText()
    {
        return "";
    }
}
