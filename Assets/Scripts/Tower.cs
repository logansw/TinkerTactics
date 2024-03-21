using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public ProjectileLauncher ProjectileLauncher;
    public TargetTracker TargetTracker;
    public TargetCalculator TargetCalculator;

    public virtual void Update()
    {
        if (CanAttack())
        {
            Attack();
        }
    }

    private bool CanAttack()
    {
        return ProjectileLauncher.CanAttack() && TargetTracker.HasEnemiesInRange();
    }

    private void Attack()
    {
        ProjectileLauncher.LaunchProjectile(TargetTracker.GetHighestPriorityTarget());
    }
}
