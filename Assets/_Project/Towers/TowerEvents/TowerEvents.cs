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
/// Called when an enemy is hit by a Tower's attack before damage calculations
/// </summary>
public class PreEnemyImpactEvent : ITowerEvent
{
    public Enemy Enemy { get; private set; }
    public ProjectileBase Projectile { get; private set; }

    public PreEnemyImpactEvent(Enemy enemy, ProjectileBase projectile)
    {
        Enemy = enemy;
        Projectile = projectile;
    }
}

/// <summary>
/// Called whenever an enemy receives damage.
/// </summary>
/// <remarks>
/// On-hit effects should be called here.
/// Primary projectile effects should NOT be called here (e.g. Pierce, Bounce)
/// </remarks>
public class EnemyDamagedEvent : ITowerEvent
{
    public Enemy Enemy { get; private set; }
    public ProjectileBase Projectile { get; private set; }

    public EnemyDamagedEvent(Enemy enemy, ProjectileBase projectile = null)
    {
        Enemy = enemy;
        Projectile = projectile;
    }
}

/// <summary>
/// Called when an enemy is killed by a Tower's attack
/// </summary>
public class EnemyDeathEvent : ITowerEvent
{
    public Enemy Enemy { get; private set; }
    public Tower Tower { get; private set; }
    
    public EnemyDeathEvent(Enemy enemy, Tower tower)
    {
        Enemy = enemy;
        Tower = tower;
    }
}

/// <summary>
/// Called when an enemy is hit by a Tower's attack after damage calculations
/// </summary>
public class PostEnemyImpactEvent : ITowerEvent
{
    public Enemy Enemy { get; private set; }
    public ProjectileBase Projectile { get; private set; }

    public PostEnemyImpactEvent(Enemy enemy, ProjectileBase projectile)
    {
        Enemy = enemy;
        Projectile = projectile;
    }
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
public class BasicAttackEvent : ITowerEvent
{
    public ProjectileBase Projectile;
    public Tower Tower;
    public Enemy Target;

    public BasicAttackEvent(ProjectileBase projectile, Tower tower, Enemy target)
    {
        Projectile = projectile;
        Tower = tower;
        Target = target;
    }
}