using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatusConditionRenderer))]
public class StatusConditionTracker : MonoBehaviour
{
    private Enemy _enemy;
    public List<StatusCondition> StatusConditionsApplied { get; private set; }
    private StatusConditionRenderer _statusConditionRenderer;
    public Action e_OnStatusConditionsChanged;

    void Awake()
    {
        StatusConditionsApplied = new List<StatusCondition>();
        _statusConditionRenderer = GetComponent<StatusConditionRenderer>();
    }
    

    public bool HasStatusCondition<T>(out T statusCondition) where T : StatusCondition
    {
        foreach (StatusCondition statusConditionCandidate in StatusConditionsApplied)
        {
            if (statusConditionCandidate is T)
            {
                statusCondition = (T)statusConditionCandidate;
                return true;
            }
        }
        statusCondition = null;
        return false;
    }

    public void AddStatusCondition<T>(float duration, int stacks) where T : StatusCondition
    {
        if (HasStatusCondition<T>(out T statusCondition))
        {
            statusCondition.AddStacks(stacks);
        }
        else
        {
            T newStatusCondition = gameObject.AddComponent<T>();
            if (newStatusCondition.CheckRules())
            {
                newStatusCondition.Initialize(duration, stacks, this);
                StatusConditionsApplied.Add(newStatusCondition);
            }
            else
            {
                Destroy(newStatusCondition);
            }
        }
        _statusConditionRenderer.RenderStatusConditions();
        e_OnStatusConditionsChanged?.Invoke();
    }

    public void RemoveStatusCondition(StatusCondition statusCondition)
    {
        StatusConditionsApplied.Remove(statusCondition);
        _statusConditionRenderer.RenderStatusConditions();
        e_OnStatusConditionsChanged?.Invoke();
    }

    public void RemoveStacks(StatusCondition statusCondition, int count)
    {
        statusCondition.RemoveStacks(count);
        _statusConditionRenderer.RenderStatusConditions();
        e_OnStatusConditionsChanged?.Invoke();
    }

    public void ClearStatusConditions()
    {
        foreach (StatusCondition statusCondition in StatusConditionsApplied)
        {
            Destroy(statusCondition);
        }
        StatusConditionsApplied.Clear();
        _statusConditionRenderer.RenderStatusConditions();
        e_OnStatusConditionsChanged?.Invoke();
    }

    public void UpdateRenderer()
    {
        _statusConditionRenderer.RenderStatusConditions();
    }

    public float ProcessMoveStatusConditions(float initialMoveSpeed)
    {
        float currentMovementSpeed = initialMoveSpeed;
        foreach (StatusCondition statusCondition in StatusConditionsApplied)
        {
            if (statusCondition is IMoveStatusCondition moveStatusCondition)
            {
                currentMovementSpeed = moveStatusCondition.OnMove(currentMovementSpeed);
            }
        }
        return currentMovementSpeed;
    }

    public void ProcessTickStatusConditions(Enemy enemy)
    {
        foreach (StatusCondition statusCondition in StatusConditionsApplied)
        {
            if (statusCondition is ITickStatusCondition tickStatusCondition)
            {
                tickStatusCondition.OnTick(enemy);
            }
        }
    }

    public float ProcessDamageStatusConditions(float initialDamage)
    {
        float damage = initialDamage;
        foreach (StatusCondition statusCondition in StatusConditionsApplied)
        {
            if (statusCondition is IDamageStatusCondition damageStatusCondition)
            {
                damage = damageStatusCondition.OnDamage(damage);
            }
        }
        return damage;
    }
}