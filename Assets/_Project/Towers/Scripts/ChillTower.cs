using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillTower : Tower
{
    int autoCount = 0;

    protected override void OnEnable()
    {
        base.OnEnable();
        EventBus.Subscribe<BasicAttackEvent>(OnBasicAttack);
        IdleState.e_OnIdleStateEnter += () => { autoCount = 0; };
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventBus.Unsubscribe<BasicAttackEvent>(OnBasicAttack);
    }

    public void OnBasicAttack(BasicAttackEvent e)
    {
        if (e.Tower != this) { return; }
        autoCount++;
        if (autoCount == 3)
        {
            e.Projectile.ProjectileEffectTracker.AddAttribute<ChillProjectileAttribute>(1);
            autoCount = 0;
        }
    }
}
