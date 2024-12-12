using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinkerExecute : TinkerBase
{
    public override string GetDescription()
    {
        return "Give Tower +5 Execute";
    }

    public override void Initialize(Tower recipient)
    {
        _tower = recipient;
        EventBus.Subscribe<TowerActionEvent>(OnProjectileSpawned);
    }

    private void OnProjectileSpawned(TowerActionEvent e)
    {
        List<Projectile> projectiles = e.Projectiles;
        foreach (Projectile projectile in projectiles)
        {
            ProjectileEffectTracker projectileEffectTracker = projectile.GetComponent<ProjectileEffectTracker>();
            projectileEffectTracker.AddEffect<ExecuteProjectileEffect>(5);
        }
    }
}