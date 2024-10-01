using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
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
