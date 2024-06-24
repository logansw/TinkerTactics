using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract component attached to towers that determines which target to aim at when attacking.
/// </summary>
public abstract class TargetCalculator : MonoBehaviour
{
    public abstract List<Enemy> PrioritizeTargets(List<Enemy> enemies);
    public abstract Enemy GetHighestPriorityTarget(List<Enemy> enemies);
}