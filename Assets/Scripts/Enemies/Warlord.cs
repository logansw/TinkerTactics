using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlord : Enemy
{
    public static Action e_OnWarlordDefeated;
    public delegate void OnWarlordRemoved(Warlord warlord);
    public OnWarlordRemoved e_OnWarlordRemoved;
    public WaveHolder WaveHolder;
    private Vector3 _startPosition;

    public void Start()
    {
        Render(false);
        HealthbarUI.s_Instance.RegisterHealth(Health);
        EffectTracker.AddEffect<EffectUntargetable>(int.MaxValue, 1);
        _collider.enabled = false;
    }

    public override void OnPathEnd()
    {
        EndReached = true;
        BattleManager.s_Instance.DamagePlayer(EnemySO.Damage);
        e_OnWarlordRemoved?.Invoke(this);
        Render(false);
        EnemyManager.s_Instance.RemoveEnemyFromList(this);
        EffectTracker.AddEffect<EffectUntargetable>(int.MaxValue, 1);
        IsSpawned = false;
        Health.TakeDamage(Health.CurrentHealth);
    }

    public void Respawn(WaveSpawner waveSpawner)
    {
        Initialize(waveSpawner.StartTile);
        EndReached = false;
        Render(true);
        EffectTracker.ClearEffects();
        IsSpawned = true;
        _startPosition = waveSpawner.transform.position;
        _collider.enabled = true;
    }

    public override void OnDeath()
    {
        base.OnDeath();
        e_OnWarlordDefeated?.Invoke();
    }

    public void Retreat()
    {
        OnRemovedFromBoard();
    }

    private void OnRemovedFromBoard()
    {
        e_OnWarlordRemoved?.Invoke(this);
        Render(true);
        EnemyManager.s_Instance.RemoveEnemyFromList(this);
        EffectTracker.AddEffect<EffectUntargetable>(int.MaxValue, 1);
        IsSpawned = false;
        StartCoroutine(AnimateRetreat());
        _collider.enabled = false;
    }

    private IEnumerator AnimateRetreat()
    {
        Vector3 endPos = _startPosition;
        while ((transform.position - endPos).magnitude > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, 3f * Time.deltaTime);
            yield return null;
        }
        Render(false);
    }
}