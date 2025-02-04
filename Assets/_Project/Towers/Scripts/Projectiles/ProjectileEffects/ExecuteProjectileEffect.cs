using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteProjectileEffect : ProjectileEffect
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
        if (hit is Warlord)
        {
            return;
        }
        if (hit.Health.CurrentHealth <= Stacks)
        {
            hit.Health.TakeDamage(9999);
        }
    }

    public override void OnProjectileLaunched(Enemy target)
    {
        // Do Nothing
    }
}