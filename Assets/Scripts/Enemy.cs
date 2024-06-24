using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public TilePath NextTilePath { get; set;}
    public bool IsDead => Health.CurrentHealth <= 0;

    public virtual void Start()
    {
        Health.e_OnHealthBreak += OnBreak;
        Health.e_OnHealthDepleted += OnDeath;
        EnemyManager.s_Instance.AddEnemy(this);
        StartCoroutine(TempDelayedStart());
    }

    // TODO: Remove this once a proper wave spawning system is implemented. Assign the enemy to the path then.
    private IEnumerator TempDelayedStart()
    {
        yield return new WaitForSeconds(0.1f);
        NextTilePath = MapGenerator.s_Instance.StartTilePath;
    }

    public virtual void OnDisable()
    {
        EnemyManager.s_Instance.RemoveEnemyFromList(this);
    }

    public virtual void Update()
    {
        if (StateController.s_Instance.CurrentState != StateType.BattleState) { return; }
        Move();
    }

    /// <summary>
    /// Called when the enemy's health reaches 0.
    /// </summary>
    /// Enemy is not destroyed immediately so that damage and kill statistics can be recorded,
    /// and so that the death animation can be played.
    public virtual void OnDeath()
    {
        e_OnUnitDeath?.Invoke(this);
        Destroy(gameObject, 3f);
    }

    public virtual void OnBreak()
    {
        gameObject.AddComponent<EffectBreak>();
        e_OnUnitBreak?.Invoke(this);
    }

    public virtual DamageData OnImpact(float incomingDamage)
    {
        foreach (float multiplier in DamageMultipliers)
        {
            incomingDamage *= multiplier;
        }
        float physicalFactor = 100f / (100f + Armor);
        float postMitigationDamage = incomingDamage * physicalFactor;
        Health.TakeDamage((float)postMitigationDamage);
        DamageData damageData = new DamageData(this, postMitigationDamage);
        return damageData;
    }

    public virtual void Move()
    {
        if (NextTilePath != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, NextTilePath.transform.position, MovementSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, NextTilePath.transform.position) < 0.1f)
            {
                NextTilePath.OnEnemyEnter(this);
            }
        }
    }
}
