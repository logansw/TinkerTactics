using UnityEngine;
using System.Collections.Generic;

public class ArcherBasicAttack : Ability
{
    [SerializeField] private int Range;
    [SerializeField] private int Damage;

    public override void Activate()
    {
        Enemy target = null;
        foreach (Enemy enemy in EnemyManager.s_Instance.Enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= Range)
            {
                if (target == null)
                {
                    target = enemy;
                }
                else
                {
                    if (enemy.DistanceTraveled > target.DistanceTraveled)
                    {
                        target = enemy;
                    }
                }
            }
        }
        target.OnImpact(Damage);
    }
}