using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TC_Fastest : TargetCalculator
{
    public override List<Enemy> PrioritizeTargets(List<Enemy> enemies)
    {
        enemies.Sort((enemy1, enemy2) => 
            enemy2.MovementSpeed.CompareTo(enemy1.MovementSpeed)
        );

        return enemies;
    }
}