using System.Collections.Generic;
using UnityEngine;

public class CannonAttack : BasicAttack
{
    [SerializeField] private ProjectileExplosive _projectileExplosive;
    public float ExplosionRadius;

    public override void Execute()
    {
        OnActionStart();
        Enemy target = Tower.RangeIndicator.GetEnemiesInRange()[0];
        ProjectileExplosive bomb = Instantiate(_projectileExplosive, Tower.transform.position, Quaternion.identity);
        ProjectileEffectTracker projectileEffectTracker = bomb.gameObject.AddComponent<ProjectileEffectTracker>();
        bomb.Initialize(Damage.Current, ProjectileSpeed, Tower, projectileEffectTracker);
        bomb.SetExplosionRadius(ExplosionRadius);
        bomb.Launch(target);
        AttackClock.Reset();
        ReloadClock.Reset();
        ChangeCurrentAmmo(-1);
        _canAttack = false;
        // EventBus.RaiseEvent<BasicAttackEvent>(new BasicAttackEvent(new List<Projectile> { bomb }));
    }
}