using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the state of the game.
/// </summary>
public class StateController : Singleton<StateController>
{
    // Internal
    public State CurrentState;
    // States
    public State PreStartState;
    public State BattleState;
    public State BuyState;
    public State VictoryState;
    public State LossState;

    private StateType _previousState;

    void Update()
    {
        CurrentState.UpdateState(this);
    }

    /// <summary>
    /// Changes the current state of the game.
    /// </summary>
    /// <param name="newState">The new state to change to.</param>
    public void ChangeState(StateType stateType)
    {
        State newState = GetState(stateType);
        if (CurrentState != null) {
            CurrentState.OnExit(this);
        }
        _previousState = CurrentState.StateType;
        CurrentState = newState;
        CurrentState.OnEnter(this);
    }

    public static StateType GetCurrentState() {
        return s_Instance.CurrentState.StateType;
    }

    public void ChangeToPreviousState() {
        ChangeState(_previousState);
    }

    public State GetState(StateType stateType)
    {
        switch (stateType)
        {
            case StateType.PreStartState:
                return PreStartState;
            case StateType.BattleState:
                return BattleState;
            case StateType.BuyState:
                return BuyState;
            case StateType.VictoryState:
                return VictoryState;
            case StateType.LossState:
                return LossState;
            default:
                return null;
        }
    }
}