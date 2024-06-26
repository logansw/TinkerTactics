using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TC_FurthestAdvanced : TargetCalculator
{
    public override List<Enemy> PrioritizeTargets(List<Enemy> enemies)
    {
        enemies.Sort((enemy1, enemy2) => 
            enemy2.transform.position.x.CompareTo(enemy1.transform.position.x)
        );

        return enemies;
    }
}