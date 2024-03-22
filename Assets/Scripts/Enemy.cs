using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public virtual void Start()
    {
        Health.e_OnHealthBreak += OnBreak;
        Health.e_OnHealthDepleted += OnDeath;
        EnemyManager.s_Instance.AddEnemyToList(this);
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
        transform.Translate(Vector3.right * MovementSpeed * Time.deltaTime);
    }
}
