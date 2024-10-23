using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    [HideInInspector] public Tower Tower;
    public string Name;
    public StatDamage Damage;
    public InternalClock AttackClock;
    public InternalClock ReloadClock;
    public StatAttackSpeed AttackSpeed;
    public StatReloadSpeed ReloadSpeed;
    public StatAmmo MaxAmmo;
    public StatInt CurrentAmmo;
    [SerializeField] private Projectile _projectilePrefab;
    protected bool _canAttack;
    public AudioSource _abilitySound;
    public string TooltipText;
    public float ProjectileSpeed;
    protected ModifierProcessor _modifierProcessor;

    public void Initialize(Tower tower)
    {
        Tower = tower;
        _modifierProcessor = tower.ModifierProcessor;
        _modifierProcessor.e_OnModifierAdded += SetClocks;
        AttackClock = new InternalClock(1f / _modifierProcessor.CalculateAttackSpeed(AttackSpeed));
        ReloadClock = new InternalClock(1f / _modifierProcessor.CalculateReloadSpeed(ReloadSpeed));
        AttackClock.e_OnTimerDone += SetCanAttack;
        ReloadClock.e_OnTimerDone += ReloadAmmo;
        _modifierProcessor.e_OnModifierAdded += CalculateBaseAmmo;
    }

    public virtual void Attack()
    {
        Enemy target = Tower.RangeIndicator.GetEnemiesInRange()[0];
        Projectile projectile = Instantiate(_projectilePrefab, Tower.transform.position, Quaternion.identity);
        projectile.Initialize(_modifierProcessor.CalculateDamage(Damage), ProjectileSpeed, Tower);
        projectile.Launch(target);
        _abilitySound.Play();
        AttackClock.Reset();
        CurrentAmmo.Current -= 1;
        _canAttack = false;
    }

    public string GetTooltipText()
    {
        return TooltipText;
    }

    public bool CanActivate()
    {
        return _canAttack && CurrentAmmo.Current > 0 && Tower.RangeIndicator.HasEnemyInRange();
    }

    private void SetCanAttack()
    {
        _canAttack = true;
    }

    private void ReloadAmmo()
    {
        if (CurrentAmmo.Current < MaxAmmo.CalculatedFinal)
        {
            CurrentAmmo.Base = MaxAmmo.CalculatedFinal;
            CurrentAmmo.Current += 1;
            ReloadClock.Reset();
        }
    }

    private void CalculateBaseAmmo()
    {
        _modifierProcessor.CalculateMaxAmmo(MaxAmmo);
    }

    public void SetClocks()
    {
        AttackClock.SetTimeToWait(1f / _modifierProcessor.CalculateAttackSpeed(AttackSpeed));
        ReloadClock.SetTimeToWait(1f / _modifierProcessor.CalculateReloadSpeed(ReloadSpeed));
    }
}