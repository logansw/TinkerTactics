using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    void Start()
    {
        StateController.s_Instance.ChangeState(StateType.PreStartState);
    }

    public void StartBattle()
    {
        StateController.s_Instance.ChangeState(StateType.BattleState);
    }

    public void Continue()
    {
        if (StateController.s_Instance.CurrentState.StateType == StateType.PreStartState)
        {
            StartBattle();
        }
        else if (StateController.s_Instance.CurrentState.StateType == StateType.BuyState)
        {
            StateController.s_Instance.ChangeState(StateType.BattleState);
        }
        else if (StateController.s_Instance.CurrentState.StateType == StateType.BattleState)
        {
            StateController.s_Instance.ChangeState(StateType.BuyState);
        }
        else if (StateController.s_Instance.CurrentState.StateType == StateType.GameOverState)
        {
            // Restart
        }
    }
}
