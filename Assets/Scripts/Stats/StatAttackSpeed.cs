using System;
using UnityEngine;

[Serializable]
public class StatAttackSpeed : StatFloat
{
    public override void SetBounds()
    {
        _min = 0.0f;
        _max = 10.0f;
    }
}