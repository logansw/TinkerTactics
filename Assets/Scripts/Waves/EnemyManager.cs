using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<Enemy> Enemies;
    public static Action e_OnWaveCleared;

    public void AddEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
        enemy.e_OnEnemyDeath += RemoveEnemyFromList;
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        Enemies.Remove(enemy);
        enemy.e_OnEnemyDeath -= RemoveEnemyFromList;
    }

    public void SpawnEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }
}
