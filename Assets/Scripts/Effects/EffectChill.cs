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
    }

    public float GetSpeedMultiplier()
    {
        return 0.5f;
    }
}