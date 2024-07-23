using UnityEngine;
using System.Collections.Generic;

public class ExplosiveAbility : Ability
{
    [SerializeField] private int Damage;
    [SerializeField] private int AOEDamage;
    [SerializeField] private float AOESize;

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
        foreach (Enemy enemy in EnemyManager.s_Instance.Enemies)
        {
            if (enemy == target)
            {
                continue;
            }
            if (Vector3.Distance(target.transform.position, enemy.transform.position) <= AOESize)
            {
                enemy.OnImpact(AOEDamage);
            }
        }
    }

    public override string GetTooltipText()
    {
        return $"{Name}: Deals {Damage} damage to the most travleled enemy within {Range} range. Enemies within {AOESize} of the target take {AOEDamage} damage.";
    }
}