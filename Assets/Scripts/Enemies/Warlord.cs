using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlord : Enemy
{
    public delegate void OnWarlordEnd(Warlord warlord);
    public OnWarlordEnd e_OnWarlordEnd;

    public void Start()
    {
        int randomIndex = UnityEngine.Random.Range(0, WaveSpawner.s_WaveSpawners.Count);
        WaveSpawner.s_WaveSpawners[randomIndex].RegisterWarlord(this);
        Render(false);
        HealthbarUI.s_Instance.RegisterHealth(Health);
    }

    public override void OnPathEnd()
    {
        EndReached = true;
        BattleManager.s_Instance.DamagePlayer(EnemySO.Damage);
        int randomIndex = UnityEngine.Random.Range(0, WaveSpawner.s_WaveSpawners.Count);
        e_OnWarlordEnd?.Invoke(this);
        WaveSpawner.s_WaveSpawners[randomIndex].RegisterWarlord(this);
        Render(false);
        EnemyManager.s_Instance.RemoveEnemyFromList(this);
        EffectTracker.AddEffect<EffectUntargetable>(1);
    }

    public void Respawn(WaveSpawner waveSpawner)
    {
        Initialize(waveSpawner.SpawnPoint);
        EndReached = false;
        Render(true);
        EffectTracker.ClearEffects();
    }
}