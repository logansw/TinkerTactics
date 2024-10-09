using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingState : State
{
    void Awake()
    {
        StateType = StateType.Playing;
    }

    public override void OnEnter(StateController stateController)
    {

    }

    public override void UpdateState(StateController stateController)
    {
        bool finishedSpawning = true;
        foreach (WaveSpawner waveSpawner in WaveSpawner.s_WaveSpawners)
        {
            if (!waveSpawner.FinishedSpawning)
            {
                finishedSpawning = false;
            }
        }
        if (EnemyManager.s_Instance.Enemies.Count == 0 && finishedSpawning)
        {
            BattleManager.s_Instance.FinishWave();
            BattleManager.s_Instance.AttackTilePlots();
        }
    }

    public override void OnExit(StateController stateController)
    {
        
    }
}
