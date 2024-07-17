using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveLauncher : ProjectileLauncher
{
    ExplosiveTower parentTower;
    public delegate void OnKillHandler(Enemy enemy);
    public OnKillHandler e_OnKill;

    public override void LoadProjectile()
    {
        LoadedProjectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
        LoadedProjectile.Initialize(Damage, ProjectileSpeed, this);
        LoadedProjectile.e_OnImpact += CheckForKill;
        LoadedProjectile.e_OnDestroyed += OnProjectileDestroyed;
    }

    private void CheckForKill(Enemy enemy)
    {
        if (enemy.IsDead)
        {
            e_OnKill?.Invoke(enemy);
        }
    }

    private void OnProjectileDestroyed()
    {
        LoadedProjectile.e_OnImpact -= CheckForKill;
        LoadedProjectile.e_OnDestroyed -= OnProjectileDestroyed;
    }
}
