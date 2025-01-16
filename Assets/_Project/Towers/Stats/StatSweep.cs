using System;
using UnityEngine;

[Serializable]
public class StatSweep : StatFloat
{
    public override void SetBounds()
    {
        _min = 0.0f;
        _max = 360.0f;
    }
}