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
        
    }

    public override void UpdateState(StateController stateController)
    {
        // Do nothing
    }

    public override void OnExit(StateController stateController)
    {
        Debug.Log("Spawning!");
        foreach (WaveSpawner waveSpawner in WaveSpawner.s_WaveSpawners)
        {
            Debug.Log(waveSpawner.gameObject.name);
            waveSpawner.BeginWave();
        }
    }
}
