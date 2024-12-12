using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinkerKillReload : TinkerBase
{
    public override string GetDescription()
    {
        return "Killing an enemy reloads 1 ammo";   
    }

    public override void Initialize(Tower recipient)
    {
        _tower = recipient;
        EventBus.Subscribe<EnemyDeathEvent>(OnEnemyDeath);
    }

    private void OnEnemyDeath(EnemyDeathEvent e)
    {
        if (e.Tower != _tower) { return; }

        _tower.BasicAttack.ChangeCurrentAmmo(1);
    }
}