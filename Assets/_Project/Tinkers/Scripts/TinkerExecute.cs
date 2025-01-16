using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinkerExecute : TinkerBase
{
    [SerializeField] private int _executeAmount;
    public override string GetDescription()
    {
        return $"Give Tower +{_executeAmount} Execute";
    }

    public override void Initialize(Tower recipient)
    {
        _tower = recipient;
        EventBus.Subscribe<BasicAttackEvent>(OnProjectileSpawned);
    }

    private void OnProjectileSpawned(BasicAttackEvent e)
    {
        if (e.Tower != _tower) { return; }
        e.Projectile.ProjectileEffectTracker.AddEffect<ExecuteProjectileEffect>(_executeAmount);
    }
}