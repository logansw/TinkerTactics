using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour, ITowerAction
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
    public string TooltipText;
    public float ProjectileSpeed;
    protected ModifierProcessor _modifierProcessor;
    public List<string> Tags { get; }

    public void Initialize(Tower tower)
    {
        Tower = tower;
        _modifierProcessor = tower.ModifierProcessor;
        AttackClock = new InternalClock(1f / AttackSpeed.Current, gameObject);
        ReloadClock = new InternalClock(1f / ReloadSpeed.Current, gameObject);
        AttackClock.e_OnTimerDone += SetCanAttack;
        ReloadClock.e_OnTimerDone += ReloadAmmo;
    }

    public virtual void Execute()
    {
        OnActionStart();
        Enemy target = Tower.RangeIndicator.GetEnemiesInRange()[0];
        Projectile projectile = Instantiate(_projectilePrefab, Tower.transform.position, Quaternion.identity);
        projectile.Initialize(Damage.Current, ProjectileSpeed, Tower);
        projectile.Launch(target);
        AttackClock.Reset();
        CurrentAmmo.Current -= 1;
        _canAttack = false;
    }

    public void OnActionStart()
    {
        // Not implemented
    }

    public void OnActionComplete()
    {
        // Not implemented
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
        if (CurrentAmmo.Current < MaxAmmo.Current)
        {
            CurrentAmmo.Base = MaxAmmo.Current;
            CurrentAmmo.Current += 1;
            ReloadClock.Reset();
        }
    }

    public void SetClocks()
    {
        AttackClock.SetTimeToWait(1f / AttackSpeed.Current);
        ReloadClock.SetTimeToWait(1f / ReloadSpeed.Current);
    }
}