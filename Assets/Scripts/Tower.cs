using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileLauncher)), RequireComponent(typeof(TargetTracker)), RequireComponent(typeof(TargetCalculator))]
public class Tower : MonoBehaviour, ISelectable
{
    [HideInInspector] public ProjectileLauncher ProjectileLauncher;
    [HideInInspector] public TargetTracker TargetTracker;
    [HideInInspector] public TargetCalculator TargetCalculator;

    void Awake()
    {
        ProjectileLauncher = GetComponent<ProjectileLauncher>();
        TargetTracker = GetComponent<TargetTracker>();
        TargetCalculator = GetComponent<TargetCalculator>();
    }

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

    public void OnSelect()
    {
        TargetTracker.DisplayRange(true);
    }

    public void OnDeselect()
    {
        TargetTracker.DisplayRange(false);
    }
}
