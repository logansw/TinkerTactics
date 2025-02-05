using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public static Action e_OnIdleStateEnter;
    void Awake()
    {
        StateType = StateType.Idle;
    }

    public override void OnEnter(StateController stateController)
    {
        WaveSpawnerManager.s_Instance.PrepareNextWave();
        foreach (WaveSpawner waveSpawner in WaveSpawnerManager.s_Instance.WaveSpawners)
        {
            waveSpawner.Render(true);
        }
        BattleManager.s_Instance.SetTimeScale(1f);
        e_OnIdleStateEnter?.Invoke();
    }

    public override void UpdateState(StateController stateController)
    {
        // Do nothing
    }

    public override void OnExit(StateController stateController)
    {
        BattleManager.s_Instance.UndoTimeScale();
    }
}
