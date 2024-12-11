using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EffectTracker))]
public class Enemy : MonoBehaviour
{
    public EnemySO EnemySO;
    public Health Health;
    private Healthbar _healthbar;
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
    [SerializeField] protected Collider2D _collider;
    public bool EndReached;
    private Vector3 _lastPosition;

    public virtual void Awake()
    {
        float maxHealth = EnemySO.MaxHealth;
        int breakpointCount = EnemySO.SegmentCount;
        Health = new Health(EnemySO.MaxHealth);
        BaseMovementSpeed = EnemySO.MovementSpeed;
        MovementSpeed = BaseMovementSpeed;
        Armor = EnemySO.Armor;
        _healthbar = GetComponentInChildren<Healthbar>();
        _healthbar.Initialize(Health);
        EffectTracker = GetComponent<EffectTracker>();
    }

    public void Initialize(TilePath spawnPoint)
    {
        EnemyManager.s_Instance.AddEnemy(this);
        transform.position = spawnPoint.transform.position;
        TileTarget = spawnPoint;
        IsSpawned = true;
        _lastPosition = transform.position;
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

    public virtual void ReceiveProjectile(Projectile projectile, float incomingDamage)
    {
        projectile.SourceTower.EventBus.RaiseEvent<EnemyImpactEvent>(new EnemyImpactEvent(this));
        if (EffectTracker.HasEffect<EffectVulnerable>(out EffectVulnerable effectVulnerable))
        {
            incomingDamage *= effectVulnerable.GetDamageMultiplier();
        }
        float physicalFactor = 100f / (100f + Armor);
        float postMitigationDamage = incomingDamage * physicalFactor;
        Health.TakeDamage((float)postMitigationDamage);
        projectile.SourceTower.EventBus.RaiseEvent<PostEnemyImpactEvent>(new PostEnemyImpactEvent(this));
    }

    public void Move()
    {
        if (EndReached || !IsSpawned)
        {
            return;
        }
        Vector3 destination = TileTarget.transform.position;
        Vector2 direction = (destination - transform.position).normalized;
        float currentMovementSpeed = MovementSpeed;
        if (EffectTracker.HasEffect<EffectChill>(out EffectChill effectChill))
        {
            currentMovementSpeed *= effectChill.GetSpeedMultiplier();
        }
        if (EffectTracker.HasEffect<EffectStun>(out EffectStun effectStun))
        {
            currentMovementSpeed = 0;
        }
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
}