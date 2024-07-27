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
    public static Action e_OnPlayerTurnStart;
    public static Action e_OnPlayerTurnEnd;
    public static Action e_OnEnemyTurnStart;
    public static Action e_OnEnemyTurnEnd;

    void Start()
    {
        StateController.s_Instance.ChangeState(StateType.PlayerTurnState);
    }
    
    public void Continue()
    {
        if (StateController.CurrentState.Equals(StateType.PlayerTurnState))
        {
            StateController.s_Instance.ChangeState(StateType.EnemyTurnState);
        }
        else if (StateController.CurrentState.Equals(StateType.Victory))
        {
            WaveSpawner.s_Instance.BeginWave();
            StateController.s_Instance.ChangeState(StateType.EnemyTurnState);
        }
    }
}
