using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedState : State
{
    void Awake()
    {
        StateType = StateType.Paused;
    }

    public override void OnEnter(StateController stateController)
    {
        Time.timeScale = 0;
    }

    public override void UpdateState(StateController stateController)
    {
        // Do nothing
    }

    public override void OnExit(StateController stateController)
    {
        Time.timeScale = 1f;
    }
}
