using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : State
{
    public static Action e_OnBattleStart;

    void Awake()
    {
        StateType = StateType.BattleState;
    }

    public override void OnEnter(StateController stateController)
    {
        e_OnBattleStart?.Invoke();
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
