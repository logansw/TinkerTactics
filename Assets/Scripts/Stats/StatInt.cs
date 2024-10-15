using System;
using UnityEngine;

[Serializable]
public class StatInt : StatBase<int>
{
    public override void ModifyStat(int amount)
    {
        Current += amount;
    }

    public override int Clamp(int value, int min, int max)
    {
        return Mathf.Clamp(value, min, max);
    }

    public override void SetBounds()
    {
        _min = 0;
        _max = int.MaxValue;
    }
}