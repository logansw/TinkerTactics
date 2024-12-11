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
        recipient.EventBus.Subscribe<PostEnemyImpactEvent>(OnPostEnemyImpact);   
    }

    public void OnPostEnemyImpact(PostEnemyImpactEvent postEnemyImpactEvent)
    {
        if (postEnemyImpactEvent.Enemy is Warlord)
        {
            return;
        }
        if (postEnemyImpactEvent.Enemy.Health.CurrentHealth <= postEnemyImpactEvent.Enemy.Health.MaxHealth * 0.12f)
        {
            postEnemyImpactEvent.Enemy.Health.TakeDamage(9999);
        }
    }
}