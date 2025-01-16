using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryState : State
{
    void Awake()
    {
        StateType = StateType.Victory;
    }

    public override void OnEnter(StateController stateController)
    {
        CurrencyManager.s_Instance.AddPassiveCurrency();
        MarketplaceManager.s_Instance.PopulateAvailableItems();
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
