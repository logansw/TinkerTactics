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
    [HideInInspector] public int DistanceTraveled;
    [HideInInspector] public EffectTracker EffectTracker;

    public delegate void EnemyAction(Enemy enemy);
    public EnemyAction e_OnEnemyDeath;
    public EnemyAction e_OnEnemyBreak;
    [SerializeField] private AudioSource _deathSound;
    [SerializeField] private AudioSource _spawnSound;
    [SerializeField] private Collider2D _collider;

    public virtual void Awake()
    {
        float maxHealth = EnemySO.MaxHealth;
        int breakpointCount = EnemySO.SegmentCount;
        Health = new Health(EnemySO.MaxHealth, EnemySO.SegmentCount, this);
        BaseMovementSpeed = EnemySO.MovementSpeed;
        MovementSpeed = BaseMovementSpeed;
        Armor = EnemySO.Armor;
        _healthbar = GetComponentInChildren<Healthbar>();
        _healthbar.Initialize(Health);
        EffectTracker = GetComponent<EffectTracker>();
    }

    public virtual void Start()
    {
        EnemyManager.s_Instance.AddEnemy(this);
        _spawnSound.Play();
    }

    public void Initialize(TilePath spawnPoint)
    {
        transform.position = spawnPoint.transform.position;
        TileTarget = spawnPoint;
    }

    void Update()
    {
        Move();
    }

    public virtual void OnEnable()
    {
        Health.e_OnHealthBreak += OnBreak;
        Health.e_OnHealthDepleted += OnDeath;
    }

    public virtual void OnDisable()
    {
        EnemyManager.s_Instance.RemoveEnemyFromList(this);
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
        Health.e_OnHealthDepleted -= OnDeath;
        e_OnEnemyDeath?.Invoke(this);
        _collider.enabled = false;
        Destroy(gameObject, 1f);
        _deathSound.Play();
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child == transform) { continue; }
            child.gameObject.SetActive(false);
        }
    }

    public virtual void OnBreak()
    {
        EffectTracker.AddEffect<EffectBreak>(1);
        e_OnEnemyBreak?.Invoke(this);
    }

    public virtual void OnImpact(float incomingDamage)
    {
        if (EffectTracker.HasEffect<EffectVulnerable>(out EffectVulnerable effectVulnerable))
        {
            incomingDamage *= effectVulnerable.GetDamageMultiplier();
        }
        float physicalFactor = 100f / (100f + Armor);
        float postMitigationDamage = incomingDamage * physicalFactor;
        Health.TakeDamage((float)postMitigationDamage);
    }

    public void Move()
    {
        Vector3 destination = TileTarget == null ? Vector3.zero : TileTarget.transform.position;
        Vector2 direction = (destination - transform.position).normalized;
        transform.Translate(direction * Time.deltaTime * MovementSpeed / 10);
        if (TileTarget.PathType.Equals(PathType.End) && Vector2.Distance(transform.position, destination) < 0.1f)
        {
            Health.TakeDamage(Health.CurrentHealth);
        }
    }
}