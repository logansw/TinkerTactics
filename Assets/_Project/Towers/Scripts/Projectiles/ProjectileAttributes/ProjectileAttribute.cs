using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileAttribute : MonoBehaviour
{
    public int Stacks { get; set; }
    public ProjectileBase ParentProjectile;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="hit"></param>
    public abstract void OnProjectileHitPreImpact(Enemy hit);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hit"></param>
    public abstract void OnProjectileHitPostImpact(Enemy hit);

    /// <summary>
    /// Called when an enemy is damaged by any 
    /// </summary>
    /// <param name="hit"></param>
    public abstract void OnEnemyDamaged(Enemy hit);

    public abstract void OnProjectileLaunched(Enemy target);
}