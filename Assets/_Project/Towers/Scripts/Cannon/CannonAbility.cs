using UnityEngine;

public class CannonAbility : TowerAbility
{
    public float ProjectileSpeed;
    [SerializeField] private Cannon _cannon;
    [SerializeField] private GameObject _projectilePrefab;
    public override bool CanActivate()
    {
        return _abilityReloaded && _tower.RangeIndicator.HasEnemyInRange();
    }

    public override void OnActionStart()
    {
        // Implement the method
    }

    public override void Execute()
    {
        OnActionStart();
        Enemy target = _tower.RangeIndicator.GetEnemiesInRange()[0];
        GameObject projectile = Instantiate(_projectilePrefab, _tower.transform.position, Quaternion.identity).gameObject;
        ProjectileEffectTracker projectileEffectTracker = projectile.AddComponent<ProjectileEffectTracker>();
        ProjectileBallistic projectileBallistic = projectile.AddComponent<ProjectileBallistic>();
        projectileBallistic.Initialize(_tower, projectileEffectTracker, _tower.Damage.Current, ProjectileSpeed, target.transform.position - transform.position, 10f);
        projectileEffectTracker.AddEffect<BlastProjectileEffect>(_cannon.ExplosionRadius * 2);
        
        InternalClock.Reset();
        _abilityReloaded = false;
        OnActionComplete();
    }
    public override void OnActionComplete()
    {
        // Implement the method
    }
}