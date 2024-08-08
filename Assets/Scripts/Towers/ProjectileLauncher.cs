using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Launches projectiles at enemies. Handles reload and tracks stats for the tower.
/// </summary>
public abstract class ProjectileLauncher : MonoBehaviour
{
    public float BaseDamage;
    public float Damage;
    public float ProjectileSpeed;
    public Projectile ProjectilePrefab;
    protected Projectile LoadedProjectile;

    /// <summary>
    /// Instantiate the projectile prefab and set its stats.
    /// </summary>
    /// <returns>Return the instantiated projectile to be launched</returns>
    public abstract Projectile LoadProjectile();

    public void LaunchProjectile(Enemy target)
    {
        LoadProjectile().Launch(target);
    }
}
