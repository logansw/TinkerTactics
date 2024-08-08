using Unity.VisualScripting;
using UnityEngine;

public class CannonLauncher : ProjectileLauncher
{
    [SerializeField] private ProjectileExplosive _projectileExplosivePrefab;

    public override Projectile LoadProjectile()
    {
        ProjectileExplosive projectileExplosive = Instantiate(_projectileExplosivePrefab, transform.position, Quaternion.identity);
        projectileExplosive.Initialize(Damage, ProjectileSpeed, this);
        projectileExplosive.ExplosionRadius = 1f;
        return projectileExplosive;
    }
}