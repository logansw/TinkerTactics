using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PierceProjectileEffect : ProjectileEffect
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="hit"></param>
    /// <remarks>When Stacks hits 0, the Projectile is cleaned up in Projectile.OnImpact()</remarks>
    public override void OnProjectileHitPreImpact(Enemy hit)
    {
        Stacks--;
    }

    public override void OnProjectileHitPostImpact(Enemy hit)
    {
        // Do Nothing
    }

    public override void OnEnemyDamaged(Enemy hit)
    {
        // Do Nothing
    }

    public override void OnProjectileLaunched(Enemy target)
    {
        Vector2 _direction = (target.transform.position - gameObject.transform.position).normalized;
        // Rotate to match direction
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}