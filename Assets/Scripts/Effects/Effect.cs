using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public int Duration;
    public Unit Unit;

    void OnEnable()
    {
        BattleManager.e_OnEnemyTurnEnd += OnEnemyTurnEnd;
    }

    public void OnDisable()
    {
        BattleManager.e_OnEnemyTurnEnd -= OnEnemyTurnEnd;
    }

    public virtual void OnEnemyTurnEnd()
    {
        Duration--;
        if (Duration <= 0)
        {
            Remove();
        }
    }

    public virtual void Remove()
    {
        Destroy(this);
    }

}
