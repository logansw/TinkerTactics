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
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        Enemies.Remove(enemy);
        enemy.e_OnEnemyDeath -= RemoveEnemyFromList;
    }

    public void SpawnEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.position = MapManager.s_Instance.TileTarget.transform.position;
    }
}
