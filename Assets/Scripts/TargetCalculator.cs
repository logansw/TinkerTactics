using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetCalculator : MonoBehaviour
{
    public abstract List<Enemy> PrioritizeTargets(List<Enemy> enemies);
    public abstract Enemy GetHighestPriorityTarget(List<Enemy> enemies);
}