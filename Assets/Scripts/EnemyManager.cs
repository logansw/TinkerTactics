using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<Enemy> Enemies;

    public void AddEnemyToList(Enemy enemy)
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
}
