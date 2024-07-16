using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ArcherLauncher))]
public class Archer : Tower
{
    private ArcherLauncher archerLauncher;

    protected override void Awake()
    {
        base.Awake();
        archerLauncher = GetComponent<ArcherLauncher>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        archerLauncher.LaunchProjectile(TargetTracker.GetHighestPriorityTarget());
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