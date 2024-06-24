using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of enemies that can be targetted by the tower this component is attached to.
/// </summary>
[RequireComponent(typeof(TargetCalculator))]
public class TargetTracker : MonoBehaviour
{
    public List<Enemy> EnemiesInRange;
    public float Range;
    [HideInInspector] public TargetCalculator TargetCalculator;
    [SerializeField] private GameObject _rangeIndicator;

    public virtual void Awake()
    {
        TargetCalculator = GetComponent<TargetCalculator>();
    }

    public virtual void Start()
    {
        EnemiesInRange = new List<Enemy>();
    }

    public virtual void Update()
    {
        foreach (Enemy enemy in EnemyManager.s_Instance.Enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= Range)
            {
                if (!EnemiesInRange.Contains(enemy))
                {
                    AddEnemyToList(enemy);
                }
            }
            else
            {
                if (EnemiesInRange.Contains(enemy))
                {
                    RemoveEnemyFromList(enemy);
                }
            }
        }
    }

    public void AddEnemyToList(Enemy enemy)
    {
        EnemiesInRange.Add(enemy);
        enemy.e_OnUnitDeath += RemoveEnemyFromList;
    }

    public void RemoveEnemyFromList(Unit unit)
    {
        if (unit is Enemy enemy) {
            EnemiesInRange.Remove(enemy);
            enemy.e_OnUnitDeath -= RemoveEnemyFromList;
        } else {
            throw new System.Exception("Unit is not an Enemy");
        }
    }

    public bool HasEnemiesInRange()
    {
        return EnemiesInRange.Count > 0;
    }

    public Enemy GetHighestPriorityTarget()
    {
        EnemiesInRange = TargetCalculator.PrioritizeTargets(EnemiesInRange);
        return TargetCalculator.GetHighestPriorityTarget(EnemiesInRange);
    }

    public void DisplayRange(bool active)
    {
        _rangeIndicator.transform.localScale = new Vector3(Range * 2f, Range * 2f, 1f);
        _rangeIndicator.SetActive(active);
    }
}