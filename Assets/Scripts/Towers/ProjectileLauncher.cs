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

    public abstract void LoadProjectile();

    public void LaunchProjectile(Enemy target)
    {
        LoadProjectile();
        LoadedProjectile.Launch(target);
    }
}
