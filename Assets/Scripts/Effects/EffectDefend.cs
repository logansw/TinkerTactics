using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectDefend : Effect
{
    public override void Initialize(int val)
    {
        base.Initialize(val);
        Duration = 1;
        AddStacks(val);
        IconColor = new Color32(83, 191, 237, 255);
    }

    public override void OnEnable()
    {
        BattleManager.e_OnPlayerTurnEnd += OnEnemyTurnEnd;
    }

    public override void OnDisable()
    {
        BattleManager.e_OnPlayerTurnEnd -= OnEnemyTurnEnd;
    }

    public override void AddStacks(int count)
    {
        Stacks += count;
    }

    public override void RemoveStacks(int count)
    {
        Stacks -= count;
        if (Stacks <= 0)
        {
            Remove();
        }
    }

    public override string GetStackText()
    {
        return Stacks.ToString();
    }

    public override string GetAbbreviationText()
    {
        return "DEF";
    }

    public override string GetDescriptionText()
    {
        return $"DEFENSE: Absorbs {Stacks} damage.";
    }
}