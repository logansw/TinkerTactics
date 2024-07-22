using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the game flow within a single battle.
/// Keeps track of the current turn and sends commands to towers and enemies each turn.
/// </summary>
public class BattleManager : MonoBehaviour
{
    public static Action e_OnPlayerTurnEnd;
    public static Action e_OnEnemyTurnEnd;

    void Start()
    {
        StateController.s_Instance.ChangeState(StateType.Victory);
    }
    
    public void Continue()
    {
        if (StateController.CurrentState.Equals(StateType.PlayerTurnState))
        {
            e_OnPlayerTurnEnd?.Invoke();
            StateController.s_Instance.ChangeState(StateType.EnemyTurnState);
        }
        else if (StateController.CurrentState.Equals(StateType.EnemyTurnState))
        {
            e_OnEnemyTurnEnd?.Invoke();
            if (EnemyManager.s_Instance.Enemies.Count == 0)
            {
                StateController.s_Instance.ChangeState(StateType.Victory);
            }
            else
            {
                StateController.s_Instance.ChangeState(StateType.PlayerTurnState);
            }
        }
        else if (StateController.CurrentState.Equals(StateType.Victory))
        {
            WaveSpawner.s_Instance.BeginWave();
            StateController.s_Instance.ChangeState(StateType.EnemyTurnState);
        }
    }
}
