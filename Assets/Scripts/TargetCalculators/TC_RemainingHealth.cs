using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TC_RemainingHealth : TargetCalculator
{
    public override List<Enemy> PrioritizeTargets(List<Enemy> enemies)
    {
        enemies.Sort((enemy1, enemy2) => 
            enemy2.Health.CurrentHealth.CompareTo(enemy1.Health.CurrentHealth)
        );

        return enemies;
    }

    public override Enemy GetHighestPriorityTarget(List<Enemy> enemies)
    {
        return enemies[0];
    }
}
