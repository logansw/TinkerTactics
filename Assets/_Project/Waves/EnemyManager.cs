using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<Enemy> Enemies;
    [SerializeField] private List<Warlord> _warlords = new List<Warlord>();
    public Warlord CurrentWarlord { get; private set; }
    public static Action e_OnWaveCleared;

    public void Initialize(int level)
    {
        base.Initialize();
        CurrentWarlord = Instantiate(_warlords[level], transform);
    }

    public void AddEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
        enemy.e_OnEnemyDeath += RemoveEnemyFromList;
        enemy.transform.SetParent(transform);
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
