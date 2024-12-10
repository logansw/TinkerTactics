using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Marker interface for all tower events. Used to categorize and handle various event types uniformly.
/// </summary>
public interface ITowerEvent { }

/// <summary>
/// Called when the Tinker is equipped to a Tower
/// </summary>
public class TinkerEquippedEvent : ITowerEvent
{
    public Tower Tower { get; private set; }

    public TinkerEquippedEvent(Tower tower)
    {
        Tower = tower;
    }
}

/// <summary>
/// Called when an enemy is hit by a Tower's attack
/// </summary>
public class EnemyImpactEvent : ITowerEvent
{

}

/// <summary>
/// Called when an enemy is killed by a Tower's attack
/// </summary>
public class EnemyKillEvent : ITowerEvent
{

}

/// <summary>
/// Called upon wave start and end, and level start and end
/// </summary>
public class WaveProgressionEvent : ITowerEvent
{

}

/// <summary>
/// Called whenever an enemy enters or exits the range of a Tower
/// </summary>
public class EnemyRangeEvent : ITowerEvent
{

}

/// <summary>
/// Called whenever a Tower is deployed, repositioned, rotated, or recalled
/// </summary>
public class TowerPositionEvent : ITowerEvent
{

}

/// <summary>
/// Called when a Tower uses its basic attack or ability
/// </summary>
public class TowerActionEvent : ITowerEvent
{

}