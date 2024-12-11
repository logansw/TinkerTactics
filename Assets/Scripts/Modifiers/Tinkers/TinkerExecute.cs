using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinkerExecute : TinkerBase
{
    public override string GetDescription()
    {
        return "Execute non-Boss Enemies below 12% Max Health";
    }

    public override void Initialize(Tower recipient)
    {
        _tower = recipient;
        EventBus.Subscribe<PostEnemyImpactEvent>(OnPostEnemyImpact);   
    }

    public void OnPostEnemyImpact(PostEnemyImpactEvent postEnemyImpactEvent)
    {
        Debug.Log("Post Impact");
        if (postEnemyImpactEvent.Enemy is Warlord || postEnemyImpactEvent.Projectile.SourceTower != _tower)
        {
            Debug.Log("Return");
            return;
        }
        if (postEnemyImpactEvent.Enemy.Health.CurrentHealth <= postEnemyImpactEvent.Enemy.Health.MaxHealth * 0.12f)
        {
            Debug.Log("Execute");
            postEnemyImpactEvent.Enemy.Health.TakeDamage(9999);
        }
    }
}