using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LaserPointerTower : Tower
{
    public int Pierce;
    [SerializeField] private GameObject _projectilePrefab;
    public int ProjectileSpeed;
    private InternalClock _abilityClock;
    public float AbilityCooldown;
    private bool _abilityReady;

    protected override void Update()
    {
        base.Update();
        if (_abilityReady && RangeIndicator.HasEnemyInRange())
        {
            ExecuteAbility();
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        SetAbilityReady();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        EventBus.Subscribe<BasicAttackEvent>(OnTowerAction);
        _abilityClock = new InternalClock(AbilityCooldown, gameObject);
        _abilityClock.e_OnTimerDone += SetAbilityReady;
        _abilityBar.RegisterClock(_abilityClock);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventBus.Unsubscribe<BasicAttackEvent>(OnTowerAction);
        _abilityClock.Delete();
    }

    private void OnTowerAction(BasicAttackEvent e)
    {
        if (e.Tower != this) { return; }
        e.Projectile.ProjectileEffectTracker.AddEffect<PierceProjectileEffect>(Pierce);
    }

    public void ExecuteAbility()
    {
        Enemy target = RangeIndicator.GetEnemiesInRange()[0];
        GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity).gameObject;
        ProjectileEffectTracker projectileEffectTracker = projectile.AddComponent<ProjectileEffectTracker>();
        ProjectileBallistic projectileBallistic = projectile.AddComponent<ProjectileBallistic>();
        projectileBallistic.Initialize(this, projectileEffectTracker, Damage.Current * 2, ProjectileSpeed, target.transform.position - transform.position, 10f);
        projectileEffectTracker.AddEffect<PierceProjectileEffect>(Pierce * 2);
        
        _abilityClock.Paused = false;
        _abilityReady = false;
        _abilityBar.Locked = false;
        _abilityClock.Reset();
    }

    private void SetAbilityReady()
    {
        _abilityBar.SetFill(1f);
        _abilityBar.Locked = true;
        _abilityClock.Paused = true;
        _abilityReady = true;
    }
}