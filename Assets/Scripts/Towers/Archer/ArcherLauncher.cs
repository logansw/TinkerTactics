using Unity.VisualScripting;
using UnityEngine;

public class ArcherLauncher : ProjectileLauncher
{
    [SerializeField] private ProjectileArrow _projectileArrowPrefab;

    public override Projectile LoadProjectile()
    {
        ProjectileArrow projectileArrow = Instantiate(_projectileArrowPrefab, transform.position, Quaternion.identity);
        projectileArrow.Initialize(Damage, ProjectileSpeed, this);
        return projectileArrow;
    }
}