using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectStun : Effect
{
    public override void Initialize(int duration)
    {
        base.Initialize(duration);
        if (Enemy.EffectTracker.HasEffect<EffectUnstoppable>(out EffectUnstoppable effectUnstoppable))
        {
            Remove();
        }
        Duration = duration;
    }

    public override void Remove()
    {
        base.Remove();
    }
}
