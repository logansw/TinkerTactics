using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TC_FurthestAdvanced : TargetCalculator
{
    public override List<Enemy> PrioritizeTargets(List<Enemy> enemies)
    {
        enemies.Sort((enemy1, enemy2) => 
            enemy2.DistanceTraveled.CompareTo(enemy1.DistanceTraveled)
        );

        return enemies;
    }
}
