using UnityEngine;

public class Cannon : Tower
{
    public int ExplosionRadius;
    [SerializeField] private GameObject _projectilePrefab;
    public int ProjectileSpeed;
    private InternalClock _abilityClock;
    public float AbilityCooldown;


    protected override void OnEnable()
    {
        base.OnEnable();
        EventBus.Subscribe<BasicAttackEvent>(OnTowerAction);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventBus.Unsubscribe<BasicAttackEvent>(OnTowerAction);
    }

    public void OnTowerAction(BasicAttackEvent e)
    {
        if (e.Tower != this) { return; }
        e.Projectile.ProjectileEffectTracker.AddAttribute<BlastProjectileAttribute>(ExplosionRadius);
    }
}