using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    [HideInInspector] public Tower Tower;
    public string Name;
    public float Damage;
    public InternalClock AttackClock;
    public InternalClock AmmoClock;
    public StatAttackSpeed AttackSpeed;
    public StatReloadSpeed ReloadSpeed;
    public StatAmmo Ammo;
    [SerializeField] private Projectile _projectilePrefab;
    protected bool _canAttack;
    public AudioSource _abilitySound;
    public string TooltipText;
    public float ProjectileSpeed;

    public virtual void Attack()
    {
        Enemy target = Tower.RangeIndicator.GetEnemiesInRange()[0];
        Projectile projectile = Instantiate(_projectilePrefab, Tower.transform.position, Quaternion.identity);
        projectile.Initialize(Damage, ProjectileSpeed, Tower);
        projectile.Launch(target);
        _abilitySound.Play();
        AttackClock.Reset();
        AmmoClock.Reset();
        Ammo.Current -= 1;
        _canAttack = false;
    }

    public string GetTooltipText()
    {
        return TooltipText;
    }

    public void Initialize(Tower tower)
    {
        AttackSpeed.Initialize();
        ReloadSpeed.Initialize();
        Ammo.Initialize();
        Tower = tower;
        AttackClock = new InternalClock(1f / AttackSpeed.Current);
        AmmoClock = new InternalClock(1f / ReloadSpeed.Current);
        AttackClock.e_OnTimerDone += SetCanAttack;
        AmmoClock.e_OnTimerDone += ReloadAmmo;
    }

    public bool CanActivate()
    {
        return _canAttack && Ammo.Current > 0 && Tower.RangeIndicator.HasEnemyInRange;
    }

    private void SetCanAttack()
    {
        _canAttack = true;
    }

    private void ReloadAmmo()
    {
        if (Ammo.Current < Ammo.Base)
        {
            Ammo.Current += 1;
        }
    }
}
