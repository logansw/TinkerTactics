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
        WaveSpawner.s_Instance.BeginWave();
    }

    public override void UpdateState(StateController stateController)
    {
        // Do nothing
    }

    public override void OnExit(StateController stateController)
    {
        // Do nothing
    }
}
