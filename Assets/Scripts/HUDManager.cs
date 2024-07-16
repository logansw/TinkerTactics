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
    }

    public void HideTowerInformation(Tower tower)
    {
        tower.TargetTracker.DisplayRange(false);
    }
}