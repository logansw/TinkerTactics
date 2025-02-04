using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TargetCalculator
{
    /// <summary>
    /// Return list of enemies within range of the transform position
    /// </summary>
    public static List<Enemy> GetEnemiesInRange(Vector2 position, float range)
    {
        List<Enemy> enemies = new List<Enemy>();
        foreach (Enemy enemy in EnemyManager.s_Instance.Enemies)
        {
            if (!enemy.gameObject.activeInHierarchy) { continue; }
            if (Vector3.Distance(position, enemy.transform.position) <= range)
            {
                if (enemy.EffectTracker.HasEffect<EffectUntargetable>(out _))
                {
                    continue;
                }
                enemies.Add(enemy);
            }
        }
        return enemies;
    }

    /// <summary>
    /// Prioritizes targets that are closest to the tower.
    /// </summary>
    public static List<Enemy> GetClosest(List<Enemy> enemies, Tower tower)
    {
        enemies.Sort(CompareDistanceToTower);

        // Comparison function to sort enemies based on distance to tower
        int CompareDistanceToTower(Enemy enemy1, Enemy enemy2)
        {
            if (enemy1 == null)
            {
                return 1;
            }
            if (enemy2 == null)
            {
                return -1;
            }
            Vector2 towerPosition = tower.transform.position;
            float distance1 = Vector2.Distance(towerPosition, enemy1.transform.position);
            float distance2 = Vector2.Distance(towerPosition, enemy2.transform.position);
            return distance1.CompareTo(distance2);
        }
        return enemies;
    }

    /// <summary>
    /// Prioritizes the targets with the greatest movement speed
    /// </summary>
    public static List<Enemy> GetFastest(List<Enemy> enemies)
    {
        enemies.Sort((enemy1, enemy2) => 
            enemy2.MovementSpeed.CompareTo(enemy1.MovementSpeed));
        return enemies;
    }

    /// <summary>
    /// Prioritizes the targets that have traveled the greatest distance
    /// </summary>
    public static List<Enemy> GetMostTraveled(List<Enemy> enemies)
    {
        enemies.Sort((enemy1, enemy2) => 
            enemy2.DistanceTraveled.CompareTo(enemy1.DistanceTraveled));
        return enemies;
    }

    /// <summary>
    /// Prioritizes the targets with the lowest remaining health
    /// </summary>
    public static List<Enemy> GetLeastHealth(List<Enemy> enemies)
    {
        enemies.Sort((enemy1, enemy2) => 
            enemy1.Health.CurrentHealth.CompareTo(enemy2.Health.CurrentHealth));
        return enemies;
    }

    public static List<Enemy> GetTargetable(List<Enemy> enemies)
    {
        List<Enemy> targetableEnemies = new List<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            if (!enemy.EffectTracker.HasEffect<EffectUntargetable>(out _))
            {
                targetableEnemies.Add(enemy);
            }
        }
        return targetableEnemies;
    }
}