using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour, ITowerAction
{
    [HideInInspector] public Tower Tower;
    public string Name;
    public InternalClock AttackClock;
    public InternalClock ReloadClock;
    [SerializeField] private GameObject _projectilePrefab;
    protected bool _canAttack;
    public string TooltipText;
    public float ProjectileSpeed;
    protected ModifierProcessor _modifierProcessor;
    private Transform _spriteTransform;

    public void Initialize(Tower tower)
    {
        Tower = tower;
        _modifierProcessor = tower.ModifierProcessor;
        AttackClock = new InternalClock(1f / Tower.AttackSpeed.Current, gameObject);
        ReloadClock = new InternalClock(1f / Tower.ReloadSpeed.Current, gameObject);
        AttackClock.e_OnTimerDone += SetCanAttack;
        ReloadClock.e_OnTimerDone += ReloadAmmo;
        _spriteTransform = transform.Find("RangeIndicator").Find("Sprites");
    }

    public void Tick()
    {
        if (CanActivate())
        {
            Execute();
            StartCoroutine(AnimateBasicAttack(0.2f));
        }
    }

    public virtual void Execute()
    {
        OnActionStart();
        Enemy target = Tower.RangeIndicator.GetEnemiesInRange()[0];
        GameObject projectile = Instantiate(_projectilePrefab, Tower.transform.position, Quaternion.identity).gameObject;
        ProjectileEffectTracker projectileEffectTracker = projectile.AddComponent<ProjectileEffectTracker>();
        ProjectileBallistic projectileBallistic = projectile.AddComponent<ProjectileBallistic>();
        projectileBallistic.Initialize(Tower, projectileEffectTracker, Tower.Damage.Current, ProjectileSpeed, target.transform.position - transform.position, 10f);
        
        AttackClock.Reset();
        Tower.CurrentAmmo.Current -= 1;
        _canAttack = false;
        EventBus.RaiseEvent<BasicAttackEvent>(new BasicAttackEvent(projectileBallistic, Tower, target));
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
        return _canAttack && Tower.CurrentAmmo.Current > 0 && Tower.RangeIndicator.HasEnemyInRange();
    }

    private void SetCanAttack()
    {
        _canAttack = true;
    }

    private void ReloadAmmo()
    {
        if (Tower.CurrentAmmo.Current < Tower.MaxAmmo.Current)
        {
            Tower.CurrentAmmo.Base = Tower.MaxAmmo.Current;
            Tower.CurrentAmmo.Current += 1;
            ReloadClock.Reset();
        }
    }

    public void SetClocks()
    {
        AttackClock.SetTimeToWait(1f / Tower.AttackSpeed.Current);
        ReloadClock.SetTimeToWait(1f / Tower.ReloadSpeed.Current);
    }

    /// <summary>
    /// Changes the current ammo by the given amount, clamped between 0 and MaxAmmo. This is the preferred method for changing ammo outside.
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeCurrentAmmo(int amount)
    {
        Tower.CurrentAmmo.Current = Mathf.Clamp(Tower.CurrentAmmo.Current + amount, 0, Tower.MaxAmmo.Current);
    }

    public IEnumerator AnimateBasicAttack(float animationDuration)
    {
        float timeElapsed = 0;
        while (timeElapsed < animationDuration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / animationDuration;
            if (t < 1f / 5f)
            {
                float x = t / (1f / 5f);
                _spriteTransform.localScale = Vector3.one * (0.8f + (0.6f * x));
            }
            else if (t < (2f / 5f))
            {
                float x = (t - 1f / 5f) / (1f / 5f);
                _spriteTransform.localScale = Vector3.one * (1.2f + (-0.3f * x));
            }
            else
            {
                float x = (t - 2f / 5f) / (3f / 5f);
                _spriteTransform.localScale = Vector3.one * (0.9f + (0.1f * x));
            }
            yield return null;
        }
        _spriteTransform.localScale = Vector3.one;
    }
}