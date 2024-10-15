using System;
using UnityEngine;

[Serializable]
public class StatDamage : StatFloat
{
    public override void SetBounds()
    {
        _min = 0;
        _max = float.MaxValue;
    }
}