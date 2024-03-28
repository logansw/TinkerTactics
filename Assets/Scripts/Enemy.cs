using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D))]
public class Enemy : Unit, ICollectable
{
    public TilePath NextTilePath { get; set;}
    public Rigidbody2D Rigidbody2D { get; private set; }
    public Collider2D Collider2D { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    public override void Awake()
    {
        base.Awake();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Collider2D = GetComponent<Collider2D>();
    }

    public virtual void Start()
    {
        Rigidbody2D.bodyType = RigidbodyType2D.Static;
        Health.e_OnHealthBreak += OnBreak;
        Health.e_OnHealthDepleted += OnDeath;
        EnemyManager.s_Instance.AddEnemy(this);
    }

    void OnEnable()
    {
        BattleState.e_OnBattleStart += OnBattleStart;
    }

    public virtual void OnDisable()
    {
        EnemyManager.s_Instance.RemoveEnemyFromList(this);
    }

    public virtual void Update()
    {
        if (StateController.s_Instance.CurrentState != StateType.BattleState) { return; }
    }

    public virtual void OnBattleStart()
    {
        Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    public virtual void OnDeath()
    {
        e_OnUnitDeath?.Invoke(this);
        Destroy(gameObject);
    }

    public virtual void OnBreak()
    {
        gameObject.AddComponent<EffectBreak>();
        e_OnUnitBreak?.Invoke(this);
    }

    public virtual void ReceiveDamage(float incomingDamage)
    {
        foreach (float multiplier in DamageMultipliers)
        {
            incomingDamage *= multiplier;
        }
        float postMitigationDamage = incomingDamage;
        Health.TakeDamage((float)postMitigationDamage);
    }

    public void Collect()
    {
        EnemyManager.s_Instance.RecycleEnemy(this);
    }
}
