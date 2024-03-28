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
    public StateType CurrentState { get; private set; } = StateType.None;
    // States
    [SerializeField] private State PreStartState;
    [SerializeField] private State BuyState;
    [SerializeField] private State BattleState;
    [SerializeField] private State VictoryState;
    [SerializeField] private State LossState;

    // Internal
    private StateType _previousState;

    void Update()
    {
        GetState(CurrentState).UpdateState(this);
    }

    /// <summary>
    /// Changes the current state of the game.
    /// </summary>
    /// <param name="newState">The new state to change to.</param>
    public void ChangeState(StateType stateType)
    {
        if (CurrentState != StateType.None) {
            GetState(CurrentState).OnExit(this);
        }
        _previousState = CurrentState;
        CurrentState = stateType;
        GetState(CurrentState).OnEnter(this);
    }

    public void ChangeToPreviousState() {
        ChangeState(_previousState);
    }

    private State GetState(StateType stateType)
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