using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinkerBonusDamage : TinkerBase
{
    public float DamageMultiplier;

    public override string GetDescription()
    {
        return $"Deal {DamageMultiplier * 100 - 100}% bonus damage to Boss enemies";
    }

    public override void Initialize(Tower recipient)
    {
        _tower = recipient;
        EventBus.Subscribe<PreEnemyImpactEvent>(OnEnemyImpact);
    }

    private void OnEnemyImpact(PreEnemyImpactEvent enemyImpactEvent)
    {
        if (enemyImpactEvent.Projectile.SourceTower != _tower)
        {
            return;
        }
        if (enemyImpactEvent.Enemy is Warlord)
        {
            enemyImpactEvent.Projectile.Damage *= DamageMultiplier;
        }
    }
}