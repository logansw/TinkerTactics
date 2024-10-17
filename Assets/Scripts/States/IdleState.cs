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
        foreach (WaveSpawner waveSpawner in WaveSpawner.s_WaveSpawners)
        {
            waveSpawner.Render(true);
        }
        Player.s_Instance.Energy = Player.MAX_ENERGY;
        DeckManager.s_Instance.DrawNewHand();
    }

    public override void UpdateState(StateController stateController)
    {
        // Do nothing
    }

    public override void OnExit(StateController stateController)
    {
        foreach (WaveSpawner waveSpawner in WaveSpawner.s_WaveSpawners)
        {
            waveSpawner.BeginWave();
            waveSpawner.Render(false);
        }
    }
}
