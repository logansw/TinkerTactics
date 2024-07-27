using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public int Duration;
    public Enemy Enemy;
    public Color32 IconColor;
    public int Stacks;

    public virtual void Awake()
    {
        Enemy = GetComponent<Enemy>();
    }

    public virtual void Initialize(int duration)
    {
        // Nothing by default
    }

    public abstract void AddStacks(int count);
    public abstract void RemoveStacks(int count);

    public virtual void OnEnable()
    {
        BattleManager.e_OnEnemyTurnEnd += OnEnemyTurnEnd;
    }

    public virtual void OnDisable()
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

    public virtual bool CheckRules()
    {
        return true;
    }

    public abstract string GetStackText();
    public abstract string GetAbbreviationText();
    public abstract string GetDescriptionText();
}
