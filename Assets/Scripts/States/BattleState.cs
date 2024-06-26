using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : State
{
    void Awake()
    {
        StateType = StateType.Battle;
    }

    public override void OnEnter(StateController stateController)
    {
        // Do nothing
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
