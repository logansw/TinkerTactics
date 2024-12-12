using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteProjectileEffect : ProjectileEffect
{
    public override void OnProjectileHitPostDamage(Enemy hit)
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

    public override void OnProjectileHitPreDamage(Enemy hit)
    {
        // Do Nothing
    }

    public override void OnProjectileLaunched()
    {
        // Do Nothing
    }
}