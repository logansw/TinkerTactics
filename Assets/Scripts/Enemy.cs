using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public TilePath NextTilePath { get; set;}

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
        Move();
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
        float physicalFactor = 100f / (100f + Armor);
        float postMitigationDamage = incomingDamage * physicalFactor;
        Health.TakeDamage((float)postMitigationDamage);
    }

    public virtual void Move()
    {
        // transform.Translate(Vector3.right * MovementSpeed * Time.deltaTime);
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
