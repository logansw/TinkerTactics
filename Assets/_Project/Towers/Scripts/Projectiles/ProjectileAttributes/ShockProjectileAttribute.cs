using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockProjectileAttribute : ProjectileAttribute
{
    public override void OnProjectileHitPreImpact(Enemy hit)
    {
        // Do Nothing
    }

    public override void OnProjectileHitPostImpact(Enemy hit)
    {
        // Do Nothing
    }

    public override void OnEnemyDamaged(Enemy hit)
    {
        hit.StatusConditionTracker.AddStatusCondition<StatusConditionShock>(3f, 1);
    }

    public override void OnProjectileLaunched(Enemy hit)
    {
        // Do Nothing
    }
}