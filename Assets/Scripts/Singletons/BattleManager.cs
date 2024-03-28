using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    void Start()
    {
        StateController.s_Instance.ChangeState(StateType.BuyState);
    }
    
    public void Continue()
    {
        if (StateController.s_Instance.CurrentState == StateType.PreStartState)
        {
            // TODO:
        }
        else if (StateController.s_Instance.CurrentState == StateType.BuyState)
        {
            StateController.s_Instance.ChangeState(StateType.BattleState);
        }
        else if (StateController.s_Instance.CurrentState == StateType.BattleState)
        {
            StateController.s_Instance.ChangeState(StateType.BuyState);
        }
        else if (StateController.s_Instance.CurrentState == StateType.VictoryState)
        {
            // TODO: Move to the next level
        }
        else if (StateController.s_Instance.CurrentState == StateType.LossState)
        {
            // TODO: Restart the level
        }
    }
}
