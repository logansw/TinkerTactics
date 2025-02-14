using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusCondition : MonoBehaviour
{
    public Enemy Enemy;
    public Color32 IconColor;
    public int Stacks;
    protected StatusConditionTracker _statusConditionTracker;

    public virtual void Awake()
    {
        Enemy = GetComponent<Enemy>();
    }

    public virtual void Initialize(float duration, int stacks, StatusConditionTracker statusConditionTracker)
    {
        // Nothing by default
        _statusConditionTracker = statusConditionTracker;
    }

    public abstract void AddStacks(int count);
    public abstract void RemoveStacks(int count);

    public virtual void OnEnable()
    {
    }

    public virtual void OnDisable()
    {
    }

    public virtual void Remove()
    {
        Enemy.StatusConditionTracker.RemoveStatusCondition(this);
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

public interface IMoveStatusCondition
{
    public float OnMove(float moveSpeed);
}

public interface ITickStatusCondition
{
    public void OnTick(Enemy enemy);
}

public interface IDamageStatusCondition
{
    public float OnDamage(float damage);
}