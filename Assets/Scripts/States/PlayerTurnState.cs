using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : State
{
    void Awake()
    {
        StateType = StateType.PlayerTurnState;
    }

    public override void OnEnter(StateController stateController)
    {
        BattleManager.e_OnPlayerTurnStart?.Invoke();
        HUDManager.s_Instance.DisplayAllTowerInfo();
    }

    public override void UpdateState(StateController stateController)
    {
        // Do nothing
    }

    public override void OnExit(StateController stateController)
    {
        BattleManager.e_OnPlayerTurnEnd?.Invoke();
        HUDManager.s_Instance.HideAllTowerInfo();
    }
}