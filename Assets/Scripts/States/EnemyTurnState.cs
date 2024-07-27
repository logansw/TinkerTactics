using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : State
{
    void Awake()
    {
        StateType = StateType.EnemyTurnState;
    }

    public override void OnEnter(StateController stateController)
    {
        BattleManager.e_OnEnemyTurnStart?.Invoke();
        StartCoroutine(DelayedContinue());
    }

    public override void UpdateState(StateController stateController)
    {
        // Do nothing
    }

    public override void OnExit(StateController stateController)
    {
        BattleManager.e_OnEnemyTurnEnd?.Invoke();
    }

    private IEnumerator DelayedContinue()
    {
        yield return new WaitForSeconds(1f);
        if (EnemyManager.s_Instance.Enemies.Count == 0)
        {
            StateController.s_Instance.ChangeState(StateType.Victory);
        }
        else
        {
            StateController.s_Instance.ChangeState(StateType.PlayerTurnState);
        }
    }
}