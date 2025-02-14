using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinkerPierce : TinkerBase
{
    [SerializeField] private int _pierceAmount;
    public override string GetDescription()
    {
        return $"Give Tower +{_pierceAmount} Pierce";
    }

    public override void Initialize(Tower recipient)
    {
        _tower = recipient;
        EventBus.Subscribe<BasicAttackEvent>(OnProjectileSpawned);
    }

    private void OnProjectileSpawned(BasicAttackEvent e)
    {
        if (e.Tower != _tower) { return; }
        e.Projectile.ProjectileEffectTracker.AddAttribute<PierceProjectileAttribute>(_pierceAmount);
    }
}
