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
        if (EnemyManager.s_Instance.Enemies.Count == 0 && WaveSpawner.s_Instance.FinishedSpawning)
        {
            stateController.ChangeState(StateType.Idle);
        }
    }

    public override void OnExit(StateController stateController)
    {
        
    }
}
