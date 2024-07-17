using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    void Start()
    {
        StateController.s_Instance.ChangeState(StateType.Buy);
    }
    
    public void Continue()
    {
        if (StateController.CurrentState.Equals(StateType.PreStart))
        {
            // TODO:
        }
        else if (StateController.CurrentState.Equals(StateType.Buy))
        {
            StateController.s_Instance.ChangeState(StateType.Battle);
            WaveSpawner.s_Instance.BeginWave();
        }
        else if (StateController.CurrentState.Equals(StateType.Battle))
        {
            StateController.s_Instance.ChangeState(StateType.Buy);
        }
        else if (StateController.CurrentState.Equals(StateType.Victory))
        {
            // TODO: Move to the next level
        }
        else if (StateController.CurrentState.Equals(StateType.Loss))
        {
            // TODO: Restart the level
        }
    }
}
