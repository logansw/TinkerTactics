using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public float AttackSpeed; // APS (attacks per second)
    public float Damage;
    public float ProjectileSpeed;
    private bool _reloaded;
    public Projectile ProjectilePrefab;

    void Start()
    {
        _reloaded = true;
    }

    public void LaunchProjectile(Enemy target)
    {
        Projectile projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
        projectile.Initialize(Damage, ProjectileSpeed);
        projectile.Launch(target);
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
}
