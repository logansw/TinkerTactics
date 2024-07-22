using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectStun : Effect
{
    public void Initialize(int duration)
    {
        Duration = duration;
    }

    void Awake()
    {
        Unit = GetComponent<Unit>();
        Duration = 1;
    }

    public override void Remove()
    {
        base.Remove();
    }
}
