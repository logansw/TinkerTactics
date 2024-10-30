using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlord : Enemy
{
    public static Action e_OnWarlordDefeated;
    public delegate void OnWarlordEnd(Warlord warlord);
    public OnWarlordEnd e_OnWarlordEnd;
    public WaveHolder WaveHolder;

    public void Start()
    {
        Render(false);
        HealthbarUI.s_Instance.RegisterHealth(Health);
    }

    public override void OnPathEnd()
    {
        EndReached = true;
        BattleManager.s_Instance.DamagePlayer(EnemySO.Damage);
        e_OnWarlordEnd?.Invoke(this);
        Render(false);
        EnemyManager.s_Instance.RemoveEnemyFromList(this);
        EffectTracker.AddEffect<EffectUntargetable>(1);
        IsSpawned = false;
        // Health.TakeDamage(Health.CurrentHealth);
    }

    public void Respawn(WaveSpawner waveSpawner)
    {
        Initialize(waveSpawner.StartTile);
        EndReached = false;
        Render(true);
        EffectTracker.ClearEffects();
        IsSpawned = true;
    }

    public override void OnDeath()
    {
        base.OnDeath();
        e_OnWarlordDefeated?.Invoke();
    }
}