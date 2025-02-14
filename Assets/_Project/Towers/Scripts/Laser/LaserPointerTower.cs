using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        e.Projectile.ProjectileEffectTracker.AddAttribute<PierceProjectileAttribute>(Pierce);
    }
}