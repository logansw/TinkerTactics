using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string UpgradeName;
    public string Description;
    public int Tier;

    public virtual void ApplyUpgrade(Tower tower)
    {
        Debug.LogWarning($"ApplyUpgrade not implemented");
    }
}