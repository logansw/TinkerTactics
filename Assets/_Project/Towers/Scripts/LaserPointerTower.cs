using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointerTower : Tower
{
    public int Pierce;

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

    private void OnTowerAction(BasicAttackEvent e)
    {
        if (e.Tower != this) { return; }
        Debug.Log($"Adding Pierce to {e.Projectile.name}");
        e.Projectile.ProjectileEffectTracker.AddEffect<PierceProjectileEffect>(Pierce);
    }
}