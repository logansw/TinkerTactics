using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private State IdleState;
    [SerializeField] private State PlayingState;
    [SerializeField] private State VictoryState;
    [SerializeField] private State LossState;
    [SerializeField] private TMP_Text StateText;
    [SerializeField] private State PausedState;

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
        StateText.text = CurrentState.ToString();
        Debug.Log(StateText.text);
        Debug.LogError("Test");
    }

    public void ChangeToPreviousState() {
        ChangeState(_previousState);
    }

    private State GetState(StateType stateType)
    {
        switch (stateType)
        {
            case StateType.Idle:
                return IdleState;
            case StateType.Playing:
                return PlayingState;
            case StateType.Victory:
                return VictoryState;
            case StateType.Loss:
                return LossState;
            case StateType.Paused:
                return PausedState;
            default:
                return null;
        }
    }
}