using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TargetCalculator
{
    /// <summary>
    /// Return list of enemies within range of the transform position
    /// </summary>
    public static List<Enemy> GetEnemiesInRange(Transform transform, float range)
    {
        List<Enemy> enemies = new List<Enemy>();
        foreach (Enemy enemy in EnemyManager.s_Instance.Enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= range)
            {
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
        enemies.Sort((enemy1, enemy2) => 
            Vector2.Distance(tower.transform.position, enemy1.transform.position)
            .CompareTo(Vector2.Distance(tower.transform.position, enemy2.transform.position)));
        return enemies;
    }

    /// <summary>
    /// Prioritizes the targets with the greatest movement speed
    /// </summary>
    public static List<Enemy> GetFastest(List<Enemy> enemies)
    {
        enemies.Sort((enemy1, enemy2) => 
            enemy1.MovementSpeed.CompareTo(enemy2.MovementSpeed));
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
}