using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileChill : Projectile
{
    public override void OnImpact(Enemy recipient)
    {
        base.OnImpact(recipient);
        recipient.EffectTracker.AddEffect<EffectChill>(1);
    }
}
