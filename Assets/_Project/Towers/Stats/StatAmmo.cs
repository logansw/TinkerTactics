using System;
using UnityEngine;

[Serializable]
public class StatAmmo : StatInt
{
    public override void SetBounds()
    {
        _min = 0;
        _max = int.MaxValue;
    }
}