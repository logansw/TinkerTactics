using UnityEngine;

public class CannonAttack : BasicAttack
{
    [SerializeField] private ProjectileExplosive _projectileExplosive;
    public float ExplosionRadius;

    public override void Attack()
    {
        Enemy target = Tower.RangeIndicator.GetEnemiesInRange()[0];
        ProjectileExplosive bomb = Instantiate(_projectileExplosive, Tower.transform.position, Quaternion.identity);
        bomb.Initialize(Damage.Current, ProjectileSpeed, Tower);
        bomb.SetExplosionRadius(ExplosionRadius);
        bomb.Launch(target);
        _abilitySound.Play();
        AttackClock.Reset();
        ReloadClock.Reset();
        CurrentAmmo.Current -= 1;
        _canAttack = false;
    }
}