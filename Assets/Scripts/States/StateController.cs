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
    public static StateType CurrentState { get; private set; } = StateType.None;
    // States
    [SerializeField] private State PlayerTurnState;
    [SerializeField] private State EnemyTurnState;
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
            case StateType.PlayerTurnState:
                return PlayerTurnState;
            case StateType.EnemyTurnState:
                return EnemyTurnState;
            case StateType.Victory:
                return VictoryState;
            case StateType.Loss:
                return LossState;
            default:
                return null;
        }
    }
}