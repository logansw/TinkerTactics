using UnityEngine;
using System.Collections.Generic;

public class Cannon : Tower
{
    public int ExplosionRadius;

    protected override void OnEnable()
    {
        base.OnEnable();
        EventBus.Subscribe<BasicAttackEvent>(OnTowerAction);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventBus.Unsubscribe<BasicAttackEvent>(OnTowerAction);
    }

    public void OnTowerAction(BasicAttackEvent e)
    {
        if (e.Tower != this) { return; }
        e.Projectile.ProjectileEffectTracker.AddEffect<BlastProjectileEffect>(ExplosionRadius);
    }
}