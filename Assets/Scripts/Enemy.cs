using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public TilePath NextTilePath { get; set; }
    public bool IsDead => Health.CurrentHealth <= 0;
    public int GoldValue;
    public int DistanceTraveled;

    public virtual void Start()
    {
        EnemyManager.s_Instance.AddEnemy(this);
        NextTilePath = MapManager.s_Instance.StartTile.NextTilePath;
    }

    public virtual void OnEnable()
    {
        Health.e_OnHealthBreak += OnBreak;
        Health.e_OnHealthDepleted += OnDeath;
        BattleManager.e_OnPlayerTurnEnd += Move;
    }

    public virtual void OnDisable()
    {
        EnemyManager.s_Instance.RemoveEnemyFromList(this);
        Health.e_OnHealthBreak -= OnBreak;
        Health.e_OnHealthDepleted -= OnDeath;
        BattleManager.e_OnPlayerTurnEnd -= Move;
    }

    /// <summary>
    /// Called when the enemy's health reaches 0.
    /// </summary>
    /// Enemy is not destroyed immediately so that damage and kill statistics can be recorded,
    /// and so that the death animation can be played.
    public virtual void OnDeath()
    {
        e_OnUnitDeath?.Invoke(this);
        gameObject.SetActive(false);
        Destroy(gameObject, 3f);
    }

    public virtual void OnBreak()
    {
        gameObject.AddComponent<EffectBreak>();
        e_OnUnitBreak?.Invoke(this);
    }

    public virtual void OnImpact(float incomingDamage)
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
        if (GetComponent<EffectStun>() != null)
        {
            return;
        }
        StartCoroutine(AnimateMove());
        DistanceTraveled += MovementSpeed;
    }

    private IEnumerator AnimateMove()
    {
        for (int i = 0; i < MovementSpeed; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = NextTilePath.transform.position;
            float timeElapsed = 0;
            float duration = 0.5f / MovementSpeed;
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
