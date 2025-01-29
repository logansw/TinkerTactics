using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class Archer : Tower
{
    public float AttackSpeedMultiplier;
    [SerializeField] private GameObject _projectilePrefab;
    public int ProjectileSpeed;
    private InternalClock _abilityClock;
    public float AbilityCooldown;
    private bool _abilityReady;

    public override void Initialize()
    {
        base.Initialize();
        SetAbilityReady();
    }

    protected override void Update()
    {
        base.Update();
        if (_abilityReady && RangeIndicator.HasEnemyInRange())
        {
            StartCoroutine(ExecuteAbility());
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _abilityClock = new InternalClock(AbilityCooldown, gameObject);
        _abilityClock.e_OnTimerDone += SetAbilityReady;
        _abilityBar.RegisterClock(_abilityClock);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _abilityClock.Delete();
    }

    public IEnumerator ExecuteAbility()
    {
        _abilityReady = false;
        _abilityBar.SetFill(0f);
        _abilityClock.Reset();
        CurrentAmmo.Current = MaxAmmo.Current;
        AttackSpeed.Current = AttackSpeed.Current * AttackSpeedMultiplier;
        ReloadSpeed.Current = ReloadSpeed.Current * AttackSpeedMultiplier;
        BasicAttack.AttackClock.SetTimeToWait(1f / AttackSpeed.Current);
        BasicAttack.ReloadClock.SetTimeToWait(1f / ReloadSpeed.Current);
        yield return new WaitForSeconds(4f);
        AttackSpeed.Current = AttackSpeed.Current / AttackSpeedMultiplier;
        ReloadSpeed.Current = ReloadSpeed.Current / AttackSpeedMultiplier;
        BasicAttack.AttackClock.SetTimeToWait(1f / AttackSpeed.Current);
        BasicAttack.ReloadClock.SetTimeToWait(1f / ReloadSpeed.Current);

        _abilityClock.Paused = false;
        _abilityReady = false;
        _abilityBar.Locked = false;
    }

    private void SetAbilityReady()
    {
        _abilityBar.SetFill(1f);
        _abilityBar.Locked = true;
        _abilityClock.Paused = true;
        _abilityReady = true;
    }
}