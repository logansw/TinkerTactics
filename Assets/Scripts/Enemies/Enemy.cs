using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EffectTracker))]
public abstract class Enemy : MonoBehaviour
{
    public EnemySO EnemySO;
    public Health Health;
    private Healthbar _healthbar;
    public int MovementSpeed { get; set; }
    [HideInInspector] public int BaseMovementSpeed { get; private set; }
    [HideInInspector] public float Armor;
    public TilePath NextTilePath { get; set; }
    public bool IsDead => Health.CurrentHealth <= 0;
    [HideInInspector] public int DistanceTraveled;
    [HideInInspector] public EffectTracker EffectTracker;

    public delegate void EnemyAction(Enemy enemy);
    public EnemyAction e_OnEnemyDeath;
    public EnemyAction e_OnEnemyBreak;

    public virtual void Awake()
    {
        float maxHealth = EnemySO.MaxHealth;
        int breakpointCount = EnemySO.SegmentCount;
        Health = new Health(EnemySO.MaxHealth, EnemySO.SegmentCount, this);
        BaseMovementSpeed = EnemySO.MovementSpeed;
        Armor = EnemySO.Armor;
        _healthbar = GetComponentInChildren<Healthbar>();
        _healthbar.Initialize(Health);
        EffectTracker = GetComponent<EffectTracker>();
    }

    public virtual void Start()
    {
        EnemyManager.s_Instance.AddEnemy(this);
        NextTilePath = MapManager.s_Instance.StartTile.NextTilePath;
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
        e_OnEnemyDeath?.Invoke(this);
        gameObject.SetActive(false);
        Destroy(gameObject, 3f);
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

    public void Move(int value)
    {
        StartCoroutine(AnimateMove(value));
        DistanceTraveled += value;
    }

    private IEnumerator AnimateMove(int distance)
    {
        for (int i = 0; i < distance; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = NextTilePath.transform.position;
            float timeElapsed = 0;
            float duration = 0.5f / distance;
            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                float t = timeElapsed / duration;
                transform.position = Vector3.Lerp(startPosition, endPosition, t);
                yield return null;
            }
            transform.position = endPosition;
            if (NextTilePath.NextTilePath == null)
            {
                OnDeath();
                yield break;
            }
            NextTilePath = NextTilePath.NextTilePath;
        }
    }
}
