using UnityEngine;

public class Cannon : Tower
{
    public int ExplosionRadius;
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
            ExecuteAbility();
        }
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

    public void OnTowerAction(BasicAttackEvent e)
    {
        if (e.Tower != this) { return; }
        e.Projectile.ProjectileEffectTracker.AddEffect<BlastProjectileEffect>(ExplosionRadius);
    }

    public void ExecuteAbility()
    {
        Enemy target = RangeIndicator.GetEnemiesInRange()[0];
        GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity).gameObject;
        ProjectileEffectTracker projectileEffectTracker = projectile.AddComponent<ProjectileEffectTracker>();
        ProjectileBallistic projectileBallistic = projectile.AddComponent<ProjectileBallistic>();
        projectileBallistic.Initialize(this, projectileEffectTracker, Damage.Current, ProjectileSpeed, target.transform.position - transform.position, 10f);
        projectileEffectTracker.AddEffect<BlastProjectileEffect>(ExplosionRadius * 2);
        
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