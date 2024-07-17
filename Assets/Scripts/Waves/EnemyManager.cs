using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<Enemy> Enemies;
    public static Action e_OnWaveCleared;

    void Start()
    {
        StartCoroutine(SlowUpdate());
    }

    private IEnumerator SlowUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // Wait for 2 seconds
            // Add your code here to check/update enemies
            if (StateController.CurrentState.Equals(StateType.Battle) && WaveSpawner.s_Instance.finishedSpawning)
            {
                if (Enemies.Count == 0)
                {
                    StateController.s_Instance.ChangeState(StateType.Buy);
                    e_OnWaveCleared?.Invoke();
                }
            }
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
    }

    public void RemoveEnemyFromList(Unit unit)
    {
        if (unit is Enemy enemy) {
            Enemies.Remove(enemy);
            enemy.e_OnUnitDeath -= RemoveEnemyFromList;
        } else {
            throw new System.Exception("Unit is not an Enemy");
        }
    }

    public void SpawnEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.position = MapGenerator.s_Instance.StartTilePath.transform.position;
    }
}
