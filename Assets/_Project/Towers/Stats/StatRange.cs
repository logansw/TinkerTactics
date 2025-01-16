using System;
using UnityEngine;

[Serializable]
public class StatRange : StatFloat
{
    public override void SetBounds()
    {
        _min = 0.0f;
        _max = 100.0f;
    }
}
