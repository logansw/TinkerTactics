using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public StateType StateType;
    public abstract void OnEnter(StateController stateController);
    public abstract void UpdateState(StateController stateController);
    public abstract void OnExit(StateController stateController);
}

[System.Serializable]
public enum StateType {
    PreStartState,
    BattleState,
    BuyState,
    VictoryState,
    LossState
}