using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinkerBlast : TinkerBase
{
    [SerializeField] private int _blastAmount;
    public override string GetDescription()
    {
        return $"Give Tower +{_blastAmount} Blast";
    
    }

    public override void Initialize(Tower recipient)
    {
        _tower = recipient;
        EventBus.Subscribe<BasicAttackEvent>(OnProjectileSpawned);   
    }

    private void OnProjectileSpawned(BasicAttackEvent e)
    {
        e.Projectile.ProjectileEffectTracker.AddEffect<BlastProjectileEffect>(_blastAmount);
    }
}