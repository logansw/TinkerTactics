using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAbility : Ability
{
    [Header("Stats")]
    public float ProjectileSpeed;

    [Header("References")]
    [SerializeField] private GameObject _projectilePrefab;
    private LaserPointerTower _laserTower;

    public override void Initialize()
    {
        _laserTower = GetComponent<LaserPointerTower>();
    }

    public override void Execute()
    {
        Enemy target = _tower.RangeIndicator.GetEnemiesInRange()[0];
        GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity).gameObject;
        ProjectileEffectTracker projectileEffectTracker = projectile.AddComponent<ProjectileEffectTracker>();
        ProjectileBallistic projectileBallistic = projectile.AddComponent<ProjectileBallistic>();
        projectileBallistic.Initialize(_tower, projectileEffectTracker, _tower.Damage.Current * 2, ProjectileSpeed, target.transform.position - transform.position, 10f);
        projectileEffectTracker.AddEffect<PierceProjectileEffect>(_laserTower.Pierce * 2);
        
        ResumeAbilityCD();
    }

    public override bool CanActivate()
    {
        return _abilityReady;
    }
}