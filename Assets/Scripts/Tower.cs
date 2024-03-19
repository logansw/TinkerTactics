using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public float AttackSpeed; // APS (attacks per second)
    public float AttackRange;
    public int Damage;
    public ProjectileLauncher ProjectileLauncher;
    public TargetCalculator TargetCalculator;

    
}
