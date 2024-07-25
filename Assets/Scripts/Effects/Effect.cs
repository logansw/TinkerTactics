using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public int Duration;
    public Enemy Enemy;
    public Color32 IconColor;

    public virtual void Initialize(int duration)
    {
        Enemy = GetComponent<Enemy>();
    }

    public abstract void AddStacks(int count);

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
        Enemy.EffectTracker.RemoveEffect(this);
        Destroy(this);
    }

    public abstract string GetStackText();
    public abstract string GetAbbreviationText();
    public abstract string GetDescriptionText();
}
