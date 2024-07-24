using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectChill : Effect
{
    public int Stacks;

    public override void Initialize(int duration)
    {
        base.Initialize(duration);
        Duration = duration;
        Stacks = 1;
        IconColor = new Color32(128, 249, 255, 255);
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
}