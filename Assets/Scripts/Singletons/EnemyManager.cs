using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<Enemy> Enemies;
    public Transform LeftSpawnAnchor;
    public Transform RightSpawnAnchor;

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

    public void RecycleEnemy(Enemy enemy)
    {
        float randomX = Random.Range(LeftSpawnAnchor.position.x, RightSpawnAnchor.position.x);
        enemy.transform.position = new Vector3(randomX, LeftSpawnAnchor.position.y, 0);
        enemy.Rigidbody2D.velocity = Vector2.zero;
    }
}
