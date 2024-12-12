using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public abstract class ProjectileEffect : MonoBehaviour
{
    public int Stacks { get; set; }
    public Projectile ParentProjectile;
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
}
