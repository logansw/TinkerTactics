using UnityEngine;

public class CannonAttack : BasicAttack
{
    [SerializeField] private ProjectileExplosive _projectileExplosive;
    public float ExplosionRadius;

    public override void Attack()
    {
        Enemy target = Tower.RangeIndicator.GetEnemiesInRange()[0];
        ProjectileExplosive bomb = Instantiate(_projectileExplosive, Tower.transform.position, Quaternion.identity);
        bomb.Initialize(_modifierProcessor.CalculateDamage(Damage), ProjectileSpeed, Tower);
        bomb.SetExplosionRadius(ExplosionRadius);
        bomb.Launch(target);
        AttackClock.Reset();
        ReloadClock.Reset();
        CurrentAmmo.Current -= 1;
        _canAttack = false;
    }
}