using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreStartState : State
{
    void Awake()
    {
        StateType = StateType.PreStartState;
    }

    public override void OnEnter(StateController stateController)
    {
        // Load random list of enemies and towers
    }

    public override void UpdateState(StateController stateController)
    {
        // Do nothing.
    }

    public override void OnExit(StateController stateController)
    {
        // Do nothing.
    }
}
