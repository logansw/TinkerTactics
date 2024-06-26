using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Launches projectiles at enemies. Handles reload and tracks stats for the tower.
/// </summary>
public abstract class ProjectileLauncher : MonoBehaviour
{
    public float BaseAttackSpeed;
    public float AttackSpeed; // APS (attacks per second)
    public float BaseDamage;
    public float Damage;
    public float ProjectileSpeed;
    private bool _reloaded;
    public Projectile ProjectilePrefab;
    protected Projectile LoadedProjectile;
    private LauncherStatistics _launcherStatistics;

    protected void Start()
    {
        _launcherStatistics = new LauncherStatistics();
        _reloaded = true;
    }

    public abstract void LoadProjectile();

    public void LaunchProjectile(Enemy target)
    {
        LoadProjectile();
        LoadedProjectile.Launch(target);
        _reloaded = false;
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(1 / AttackSpeed);
        _reloaded = true;
    }

    public bool CanAttack()
    {
        return _reloaded;
    }

    public void UpdateLauncherStatistics(DamageData damageData)
    {
        _launcherStatistics.TotalDamageDealt += damageData.DamageDealt;
        _launcherStatistics.TotalEnemiesKilled += damageData.ImpactReceiver.IsDead ? 1 : 0;
    }
}
