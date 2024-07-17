using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ExplosiveLauncher))]
public class ExplosiveTower : Tower
{
    private ExplosiveLauncher explosiveLauncher;

    protected override void Awake()
    {
        base.Awake();
        explosiveLauncher = GetComponent<ExplosiveLauncher>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        explosiveLauncher.LaunchProjectile(TargetTracker.GetHighestPriorityTarget());
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void ApplyUpgrade(Upgrade upgrade)
    {
        upgrade.UpgradeEffect();
        SelectedUpgrades.Add(upgrade);
        Tier += 1;
    }
}