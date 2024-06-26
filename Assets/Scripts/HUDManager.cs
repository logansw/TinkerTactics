using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : Singleton<HUDManager>
{
    public void DisplayTowerInformation(Tower tower)
    {
        tower.TargetTracker.DisplayRange(true);
        DisplayAppliedUpgrades(tower);
        DisplayAvailableUpgrades(tower);
    }

    public void HideTowerInformation(Tower tower)
    {
        tower.TargetTracker.DisplayRange(false);
    }

    public void DisplayAppliedUpgrades(Tower tower)
    {
        for (int i = 0; i < tower.SelectedUpgrades.Count; i++)
        {
            Debug.Log($"{tower.SelectedUpgrades[i].Name}: {tower.SelectedUpgrades[i].Description}");
        }
    }

    public void DisplayAvailableUpgrades(Tower tower)
    {
        List<Upgrade> availableUpgrades = tower.Upgrades[tower.Tier];
        for (int i = 0; i < availableUpgrades.Count; i++)
        {
            Upgrade upgrade = availableUpgrades[i];
            Debug.Log($"{upgrade.Name} ({upgrade.Cost}): {upgrade.Description}");
        }
    }
}