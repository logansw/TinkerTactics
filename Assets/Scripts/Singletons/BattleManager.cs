using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the game flow within a single battle.
/// Keeps track of the current turn and sends commands to towers and enemies each turn.
/// </summary>
public class BattleManager : MonoBehaviour
{
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
    }
}
