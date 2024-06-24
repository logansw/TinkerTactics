using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TC_Closest : TargetCalculator
{
    public override List<Enemy> PrioritizeTargets(List<Enemy> enemies)
    {
        enemies.Sort((enemy1, enemy2) => 
            Vector3.Distance(transform.position, enemy1.transform.position)
            .CompareTo(Vector3.Distance(transform.position, enemy2.transform.position))
        );

        return enemies;
    }

    public override Enemy GetHighestPriorityTarget(List<Enemy> enemies)
    {
        return enemies[0];
    }
}