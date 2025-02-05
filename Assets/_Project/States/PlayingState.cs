using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayingState : State
{
    public static Action e_OnPlayingStateEnter;

    void Awake()
    {
        StateType = StateType.Playing;
    }

    public override void OnEnter(StateController stateController)
    {
        e_OnPlayingStateEnter?.Invoke();
        WaveSpawnerManager.s_Instance.StartWaves();
    }

    public override void UpdateState(StateController stateController)
    {
        bool finishedSpawning = true;
        foreach (WaveSpawner waveSpawner in WaveSpawnerManager.s_Instance.WaveSpawners)
        {
            if (!waveSpawner.FinishedSpawning)
            {
                finishedSpawning = false;
            }
        }
        if (EnemyManager.s_Instance.Enemies.Count == 0 && finishedSpawning)
        {
            BattleManager.s_Instance.FinishWave();
        }
    }

    public override void OnExit(StateController stateController)
    {
        
    }
}
