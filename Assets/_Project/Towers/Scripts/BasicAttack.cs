using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour, ITowerAction
{
    private Tower _tower;
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
        _tower = tower;
        _modifierProcessor = tower.ModifierProcessor;
        AttackClock = new InternalClock(1f / _tower.AttackSpeed.Current, gameObject);
        ReloadClock = new InternalClock(1f / _tower.ReloadSpeed.Current, gameObject);
        AttackClock.e_OnTimerDone += SetCanAttack;
        ReloadClock.e_OnTimerDone += ReloadAmmo;
        _spriteTransform = transform.Find("RangeIndicator").Find("Sprites");
    }

    public virtual void Execute()
    {
        Enemy target = _tower.RangeIndicator.GetEnemiesInRange()[0];
        GameObject projectile = Instantiate(_projectilePrefab, _tower.transform.position, Quaternion.identity, _tower.transform).gameObject;
        ProjectileEffectTracker projectileEffectTracker = projectile.AddComponent<ProjectileEffectTracker>();
        ProjectileBallistic projectileBallistic = projectile.AddComponent<ProjectileBallistic>();
        projectileBallistic.Initialize(_tower, projectileEffectTracker, _tower.Damage.Current, ProjectileSpeed, target.transform.position - transform.position, 10f);
        
        AttackClock.Reset();
        _tower.Ammo.Current -= 1;
        _canAttack = false;
        EventBus.RaiseEvent<BasicAttackEvent>(new BasicAttackEvent(projectileBallistic, _tower, target));
        StartCoroutine(AnimateBasicAttack(0.2f));
    }

    public string GetTooltipText()
    {
        return TooltipText;
    }

    public bool CanActivate()
    {
        return _canAttack && _tower.Ammo.Current > 0 && _tower.RangeIndicator.HasEnemyInRange();
    }

    private void SetCanAttack()
    {
        _canAttack = true;
    }

    public void SetCannotAttack()
    {
        _canAttack = false;
    }

    private void ReloadAmmo()
    {
        _tower.Ammo.Current += 1;
        ReloadClock.Reset();
    }

    public void SetClocks()
    {
        AttackClock.SetTimeToWait(1f / _tower.AttackSpeed.Current);
        ReloadClock.SetTimeToWait(1f / _tower.ReloadSpeed.Current);
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