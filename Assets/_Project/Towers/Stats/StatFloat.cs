using System;
using UnityEngine;

[Serializable]
public class StatFloat : StatBase<float>
{
    public override void ModifyStat(float amount)
    {
        Current += amount;
    }

    public override float Clamp(float value, float min, float max)
    {
        return Mathf.Clamp(value, min, max);
    }

    public override void SetBounds()
    {
        _min = 0.0f;
        _max = float.MaxValue;
    }
}