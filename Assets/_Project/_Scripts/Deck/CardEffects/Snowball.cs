using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : DamagingSpell
{
    public float SlowDuration;

    public override void ActivateEffect()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        List<Enemy> enemies = TargetCalculator.GetEnemiesInRange(mousePos, Range);
        foreach (Enemy enemy in enemies)
        {
            enemy.Health.TakeDamage(Damage);
            enemy.StatusConditionTracker.AddStatusCondition<StatusConditionChill>(SlowDuration, 2);
        }
        Ammo.Current -= 1;
        BattleManager.s_Instance.UndoTimeScale();
    }   
}