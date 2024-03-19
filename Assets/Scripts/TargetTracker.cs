using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTracker : MonoBehaviour
{
    public List<Enemy> EnemiesInRange;
    public float Range;

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
}
