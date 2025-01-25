using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EffectTracker))]
public class Enemy : MonoBehaviour
{
    public EnemySO EnemySO;
    public Health Health;
    private IHealthIndicator _healthindicator;
    public float MovementSpeed { get; set; }
    [HideInInspector] public int BaseMovementSpeed { get; private set; }
    [HideInInspector] public float Armor;
    public TilePath TileTarget { get; set; }
    public bool IsDead => Health.CurrentHealth <= 0;
    [HideInInspector] public float DistanceTraveled;
    [HideInInspector] public EffectTracker EffectTracker;
    public bool IsSpawned;

    public delegate void EnemyAction(Enemy enemy);
    public EnemyAction e_OnEnemyDeath;
    public EnemyAction e_OnEnemyBreak;
    protected Collider2D _collider;
    public bool EndReached;
    private Vector3 _lastPosition;
    private InternalClock _internalClock;

    public virtual void Awake()
    {
        float maxHealth = EnemySO.MaxHealth;
        int breakpointCount = EnemySO.SegmentCount;
        _collider = GetComponent<Collider2D>();
        Health = new Health(EnemySO.MaxHealth);
        BaseMovementSpeed = EnemySO.MovementSpeed;
        MovementSpeed = BaseMovementSpeed;
        Armor = EnemySO.Armor;
        _healthindicator = GetComponentInChildren<IHealthIndicator>();
        _healthindicator.Initialize(Health);
        EffectTracker = GetComponent<EffectTracker>();
    }

    public void Initialize(TilePath spawnPoint)
    {
        EnemyManager.s_Instance.AddEnemy(this);
        transform.position = spawnPoint.transform.position;
        TileTarget = spawnPoint;
        IsSpawned = true;
        _lastPosition = transform.position;
        _internalClock = new InternalClock(0.5f, gameObject);
        _internalClock.e_OnTimerDone += Tick;
    }

    void Update()
    {
        if (StateController.CurrentState.Equals(StateType.Playing))
        {
            Move();
        }
   } 

    public virtual void OnEnable()
    {
        Health.e_OnHealthBreak += OnBreak;
        Health.e_OnHealthDepleted += OnDeath;
    }

    public virtual void OnDisable()
    {
        Health.e_OnHealthBreak -= OnBreak;
        Health.e_OnHealthDepleted -= OnDeath;
        _internalClock.e_OnTimerDone -= Tick;
    }

    /// <summary>
    /// Called when the enemy's health reaches 0.
    /// </summary>
    /// Enemy is not destroyed immediately so that damage and kill statistics can be recorded,
    /// and so that the death animation can be played.
    public virtual void OnDeath()
    {
        e_OnEnemyDeath?.Invoke(this);
        _collider.enabled = false;
        Destroy(gameObject, 1f);
        Render(false);
        Health.e_OnHealthDepleted -= OnDeath;
    }

    public virtual void OnBreak()
    {
        if (EffectTracker.HasEffect<EffectBreak>(out EffectBreak effectBreak))
        {
            effectBreak.Extend();
        }
        else
        {
            EffectTracker.AddEffect<EffectBreak>(5f, 1);
        }
        e_OnEnemyBreak?.Invoke(this);
    }

    /// <summary>
    /// Deal damage directly to an enemy, not through a projectile.
    /// </summary>
    /// <param name="damage">The amount of damage to deal.</param>
    /// <param name="projectile">The projectile which dealt the damage, or null if none.</param>
    /// <param name="resonsibleTower">The Tower which should be credited with the damage, or null if none.</param>
    /// <remarks>Use ReceiveProjectile() for damage dealt by projectiles instead.</remarks>
    public virtual void ReceiveDamage(float damage, ProjectileBase projectile = null, Tower responsibleTower = null)
    {
        if (EffectTracker.HasEffect<EffectInvincible>(out EffectInvincible effectInvincible))
        {
            return;
        }
        damage = EffectTracker.ProcessDamageEffects(damage);
        float physicalFactor = 100f / (100f + Armor);
        float postMitigationDamage = damage * physicalFactor;
        Health.TakeDamage((float)postMitigationDamage);
        EventBus.RaiseEvent<EnemyDamagedEvent>(new EnemyDamagedEvent(this, projectile));
        if (Health.CurrentHealth <= 0)
        {
            EventBus.RaiseEvent<EnemyDeathEvent>(new EnemyDeathEvent(this, responsibleTower));
        }
    }

    public void Move()
    {
        if (EndReached || !IsSpawned)
        {
            return;
        }
        Vector3 destination = TileTarget.transform.position;
        Vector2 direction = (destination - transform.position).normalized;
        float currentMovementSpeed = EffectTracker.ProcessMoveEffects(MovementSpeed);
        transform.Translate(direction * Time.deltaTime * currentMovementSpeed / 10);
        if (TileTarget.PathType.Equals(PathType.End) && Vector2.Distance(transform.position, destination) < 0.2f && !EndReached)
        {
            OnPathEnd();
            return;
        }
        if ((destination - transform.position).magnitude < 0.2f)
        {
            TileTarget = TileTarget.NextTilePath;
        }
        DistanceTraveled += (transform.position - _lastPosition).magnitude;
        _lastPosition = transform.position;
    }

    public virtual void OnPathEnd()
    {
        EndReached = true;
        BattleManager.s_Instance.DamagePlayer(EnemySO.Damage);
        Health.TakeDamage(Health.CurrentHealth);
    }

    protected void Render(bool active)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(active);
        }
    }

    private void Tick()
    {
        EffectTracker.ProcessTickEffects(this);
    }
}