using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAbility : Ability
{
    [Header("Stats")]
    public float ProjectileSpeed;

    [Header("References")]
    [SerializeField] private GameObject _projectilePrefab;
    private Cannon _cannon;

    public override void Initialize()
    {
        base.Initialize();
        _cannon = GetComponent<Cannon>();
    }

    public override void Execute()
    {
        Enemy target = _tower.RangeIndicator.GetEnemiesInRange()[0];
        GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity).gameObject;
        ProjectileEffectTracker projectileEffectTracker = projectile.AddComponent<ProjectileEffectTracker>();
        ProjectileBallistic projectileBallistic = projectile.AddComponent<ProjectileBallistic>();
        projectileBallistic.Initialize(_tower, projectileEffectTracker, _tower.Damage.Current, ProjectileSpeed, target.transform.position - transform.position, 10f);
        projectileEffectTracker.AddEffect<BlastProjectileEffect>(_cannon.ExplosionRadius * 2);
        
        ResumeAbilityCD();
    }

    public override bool CanActivate()
    {
        return _abilityReady;
    }
}