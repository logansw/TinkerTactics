using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sample Upgrade
[CreateAssetMenu(fileName = "BiggerGunUpgrade", menuName = "Upgrades/Shared/Tier1/BiggerGunUpgrade")]
public class BiggerGunUpgrade : UpgradeData
{
    public float DamageBoost;

    public override void ApplyUpgrade(Tower tower)
    {
        tower.Damage.Current += DamageBoost;
    }
}
