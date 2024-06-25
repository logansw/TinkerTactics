using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract component attached to towers that determines which target to aim at when attacking.
/// </summary>
public abstract class TargetCalculator : MonoBehaviour
{
    /// <summary>
    /// Defines the sorting order for this TargetCalculator. Enemies in range of the tower will be sorted according to this method.
    /// </summary>
    /// <param name="enemies">The list of enemies in range of the tower, to be sorted.</param>
    /// <returns>The sorted list of enemies. The first enemy is the highest priority target.</returns>
    public abstract List<Enemy> PrioritizeTargets(List<Enemy> enemies);
}